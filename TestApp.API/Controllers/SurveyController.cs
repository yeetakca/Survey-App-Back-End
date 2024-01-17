using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TestApp.API.Data;
using TestApp.API.Models.Domain;
using TestApp.API.Models.Domains;
using TestApp.API.Models.Requests;
using TestApp.API.Models.Requests.Survey;
using TestApp.API.Models.Responses;

namespace TestApp.API.Controllers
{
    [Route("api/survey")]
    [ApiController]
    [Authorize]
    public class SurveyController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly SurveyDbContext dbContext;

        public SurveyController(IConfiguration configuration, SurveyDbContext dbContext)
        {
            _configuration = configuration;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetMySurveys()
        {
            var principal = HttpContext.User;
            var requestingID = new Guid(principal.FindFirst(ClaimTypes.NameIdentifier).Value);
            var requestingRoleID = Int32.Parse(principal.FindFirst(ClaimTypes.GroupSid).Value);

            var surveys = dbContext.Surveys.Where(SurveyX => (SurveyX.IsActive && !SurveyX.IsDeleted) && (SurveyX.CreatorUserID == requestingID || requestingRoleID == 3))
                    .Include(SurveyX => SurveyX.CreatorUser)
                    .Include(SurveyX => SurveyX.CreatorUser.Role)
                    .Include(SurveyX => SurveyX.Type);

            var surveysResponse = surveys.Adapt<List<SurveyResponse>>();

            return Ok(surveysResponse);
        }

        [HttpGet]
        [Route("assigned")]
        public IActionResult GetAssignedSurveys()
        {
            var principal = HttpContext.User;
            var requestingID = new Guid(principal.FindFirst(ClaimTypes.NameIdentifier).Value);
            var requestingRoleID = Int32.Parse(principal.FindFirst(ClaimTypes.GroupSid).Value);

            var assignedSurveys = dbContext.Assigns.Where(AssignX => AssignX.UserID == requestingID && (AssignX.IsActive && !AssignX.IsDeleted))
                .Select(assign => new
            {
                Survey = dbContext.Surveys
                .Include(SurveyX => SurveyX.CreatorUser)
                .Include(SurveyX => SurveyX.CreatorUser.Role)
                .Include(SurveyX => SurveyX.Type)
                .FirstOrDefault(SurveyX => SurveyX.ID == assign.SurveyID && (SurveyX.IsActive && !SurveyX.IsDeleted)),
                Assign = assign.Adapt<AssignResponse>()
            });
            
            return Ok(assignedSurveys);
        }

        [HttpGet]
        [Route("{SurveyID:Guid}/assigned")]
        public IActionResult GetAssignedUsersForSurvey([FromRoute] Guid SurveyID)
        {
            var survey = dbContext.Surveys.FirstOrDefault(SurveyX => SurveyX.ID == SurveyID && (SurveyX.IsActive && !SurveyX.IsDeleted));

            if (survey == null)
            {
                return NotFound("Survey with given SurveyID not found.");
            }

            var assignedPeople = dbContext.Assigns.Where(AssignX => AssignX.SurveyID == SurveyID && (AssignX.IsActive && !AssignX.IsDeleted))
                .Include(AssignX => AssignX.CreatorUser)
                .Include(AssignX => AssignX.CreatorUser.Role)
                .Select(assign => new
            {
                User = dbContext.Users
                .Include(UserX => UserX.Role)
                .FirstOrDefault(UserX => UserX.ID == assign.UserID && (UserX.IsActive && !UserX.IsDeleted)),
                Assign = assign.Adapt<AssignResponse>()
            });

            return Ok(assignedPeople);
        }

        [HttpDelete]
        [Route("assigned/{AssignID:Guid}")]
        public IActionResult DeleteAssign([FromRoute] Guid AssignID)
        {
            var assign = dbContext.Assigns.FirstOrDefault(AssignX => AssignX.ID == AssignID && (AssignX.IsActive && !AssignX.IsDeleted));

            if (assign == null)
            {
                return NotFound("Assign with given AssignID not found.");
            }

            assign.IsDeleted = true;
            dbContext.Assigns.Update(assign);
            dbContext.SaveChanges();

            return Ok();
        }

        [HttpGet]
        [Route("{SurveyID:Guid}")]
        public IActionResult GetSurvey([FromRoute] Guid SurveyID)
        {
            var survey = dbContext.Surveys
                .Include(SurveyX => SurveyX.CreatorUser)
                .Include(SurveyX => SurveyX.CreatorUser.Role)
                .Include(SurveyX => SurveyX.Type)
                .FirstOrDefault(SurveyX => SurveyX.ID == SurveyID && (SurveyX.IsActive && !SurveyX.IsDeleted));

            if (survey == null)
            {
                return NotFound("Survey with given SurveyID not found.");
            }

            var surveyResponse = survey.Adapt<SurveyResponse>();

            return Ok(surveyResponse);
        }

        [HttpGet]
        [Route("{SurveyID:Guid}/questions")]
        public IActionResult GetSurveyQuestions([FromRoute] Guid SurveyID)
        {
            var survey = dbContext.Surveys.FirstOrDefault(SurveyX => SurveyX.ID == SurveyID && (SurveyX.IsActive && !SurveyX.IsDeleted));

            if (survey == null)
            {
                return NotFound("Survey with given SurveyID not found.");
            }

            var surveyQuestions = dbContext.Questions
                .Where(QuestionX => QuestionX.SurveyID == SurveyID && (QuestionX.IsActive && !QuestionX.IsDeleted))
                .Include(QuestionX => QuestionX.Type);

            var questionsResponse = surveyQuestions.Select(question => new
            {
                Question = question.Adapt<QuestionResponse>(),
                Choices = dbContext.QuestionChoices.Where(choice => choice.QuestionID == question.ID && (choice.IsActive && !choice.IsDeleted)).Adapt<List<QuestionChoiceResponse>>()
            });

            return Ok(questionsResponse);
        }

        [HttpPost]
        public IActionResult CreateSurvey([FromBody] SurveyCreateRequest surveyCreateRequest)
        {
            var requestingUserID = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var newSurvey = new Survey()
            {
                TypeID = surveyCreateRequest.TypeID,
                Title = surveyCreateRequest.Title,
                Description = surveyCreateRequest.Description,
                CreatorUserID = requestingUserID
            };

            dbContext.Surveys.Add(newSurvey);
            dbContext.SaveChanges();

            return Created("", newSurvey.ID);
        }

        [HttpPut]
        [Route("{SurveyID:Guid}")]
        public IActionResult UpdateSurvey([FromBody] SurveyUpdateRequest surveyUpdateRequest, [FromRoute] Guid SurveyID)
        {
            var survey = dbContext.Surveys.FirstOrDefault(SurveyX => SurveyX.ID == SurveyID && (SurveyX.IsActive && !SurveyX.IsDeleted));

            if (survey == null)
            {
                return NotFound("Survey with given SurveyID not found.");
            }

            survey.TypeID = surveyUpdateRequest.TypeID ?? survey.TypeID;
            survey.Title = surveyUpdateRequest.Title ?? survey.Title;
            survey.Description = surveyUpdateRequest.Description ?? survey.Description;
            survey.ModifiedAt = DateTime.Now;

            dbContext.Surveys.Update(survey);
            dbContext.SaveChanges();

            return Ok();
        }

        [HttpPut]
        [Route("{SurveyID:Guid}/active")]
        public IActionResult SetSurveyIsActive([FromRoute] Guid SurveyID, [FromBody] bool isActive)
        {
            var survey = dbContext.Surveys.FirstOrDefault(SurveyX => SurveyX.ID == SurveyID && (SurveyX.IsActive && !SurveyX.IsDeleted));

            if (survey == null)
            {
                return NotFound("Survey with given SurveyID not found.");
            }

            survey.IsActive = isActive;
            survey.ModifiedAt = DateTime.Now;
            dbContext.Surveys.Update(survey);
            dbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("{SurveyID:Guid}")]
        public IActionResult DeleteSurvey([FromRoute] Guid SurveyID)
        {
            var survey = dbContext.Surveys.FirstOrDefault(SurveyX => SurveyX.ID == SurveyID && (SurveyX.IsActive && !SurveyX.IsDeleted));

            if (survey == null)
            {
                return NotFound("Survey with given SurveyID not found.");
            }

            survey.IsDeleted = true;
            survey.ModifiedAt = DateTime.Now;
            dbContext.Surveys.Update(survey);
            dbContext.SaveChanges();

            return Ok();
        }

        [HttpPost]
        [Route("{SurveyID:Guid}/submit")]
        public IActionResult sumbitSurvey([FromRoute] Guid SurveyID)
        {
            var survey = dbContext.Surveys.FirstOrDefault(SurveyX => SurveyX.ID == SurveyID && (SurveyX.IsActive && !SurveyX.IsDeleted));

            if (survey == null)
            {
                return NotFound("Survey with given SurveyID not found.");
            }

            var requestingUserID = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var newSumbit = new Submit() {
                SurveyID = SurveyID,
                UserID = requestingUserID,
                CreatorUserID = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)
            };

            dbContext.Submits.Add(newSumbit);

            var assign = dbContext.Assigns.FirstOrDefault(AssignX => AssignX.SurveyID == SurveyID && AssignX.UserID == requestingUserID && (AssignX.IsActive && !AssignX.IsDeleted));

            if (assign == null)
            {
                return NotFound("An error occurred.");
            }

            assign.IsActive = false;
            dbContext.Assigns.Update(assign);

            dbContext.SaveChanges();

            return Created("", newSumbit.ID);
        }

        [HttpPost]
        [Route("{SurveyID:Guid}/assign/{UserID:Guid}")]
        public IActionResult assignSurvey([FromRoute] Guid SurveyID, [FromRoute] Guid UserID)
        {
            var survey = dbContext.Surveys.FirstOrDefault(SurveyX => SurveyX.ID == SurveyID && (SurveyX.IsActive && !SurveyX.IsDeleted));

            if (survey == null)
            {
                return NotFound("Survey with given SurveyID not found.");
            }

            var user = dbContext.Users.FirstOrDefault(UserX => UserX.ID == UserID && (UserX.IsActive && !UserX.IsDeleted));

            if (user == null)
            {
                return NotFound("User with given UserID not found.");
            }

            var requestingUserID = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var newAssign = new Assign()
            {
                SurveyID = SurveyID,
                UserID = UserID,
                CreatorUserID = requestingUserID,
            };

            dbContext.Assigns.Add(newAssign);
            dbContext.SaveChanges();

            return Created("", newAssign.ID);
        }
    }
}
