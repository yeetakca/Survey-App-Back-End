using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestApp.API.Data;
using TestApp.API.Models.Responses;
using Mapster;
using TestApp.API.Models.Requests.Question;
using TestApp.API.Models.Domains;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TestApp.API.Models.Requests.QuestionChoice;
using TestApp.API.Models.Requests.Answer;
using System.Diagnostics;

namespace TestApp.API.Controllers
{
    [Route("api/question")]
    [ApiController]
    [Authorize]
    public class QuestionController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly SurveyDbContext dbContext;

        public QuestionController(IConfiguration configuration, SurveyDbContext dbContext)
        {
            _configuration = configuration;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllQuestions()
        {
            var questions = dbContext.Questions
                .Include(QuestionX => QuestionX.Type)
                .Where(QuestionX => (QuestionX.IsActive && !QuestionX.IsDeleted));

            var questionsResponse = questions.Adapt<List<QuestionResponse>>();

            return Ok(questionsResponse);
        }

        [HttpGet]
        [Route("{QuestionID:Guid}")]
        public IActionResult GetQuestion([FromRoute] Guid QuestionID)
        {
            var question = dbContext.Questions
                .Include(QuestionX => QuestionX.Type)
                .FirstOrDefault(QuestionX => QuestionX.ID == QuestionID && (QuestionX.IsActive && !QuestionX.IsDeleted));

            if (question == null)
            {
                return NotFound("Question with given QuestionID not found.");
            }

            var questionResponse = question.Adapt<QuestionResponse>();

            return Ok(questionResponse);
        }

        [HttpPost]
        public IActionResult CreateQuestion([FromBody] QuestionCreateRequest questionCreateRequest)
        {
            var newQuestion = new Question()
            {
                SurveyID = questionCreateRequest.SurveyID,
                TypeID = questionCreateRequest.TypeID,
                Text = questionCreateRequest.Text,
                Index = questionCreateRequest.Index,
                IsRequired = questionCreateRequest.IsRequired,
                CreatorUserID = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)
            };

            dbContext.Questions.Add(newQuestion);
            dbContext.SaveChanges();

            return Created("", newQuestion.ID);
        }

        [HttpGet]
        [Route("{QuestionID:Guid}/choices")]
        public IActionResult GetQuestionChoices([FromRoute] Guid QuestionID)
        {
            var question = dbContext.Questions.FirstOrDefault(QuestionX => QuestionX.ID == QuestionID && (QuestionX.IsActive && !QuestionX.IsDeleted));

            if (question == null)
            {
                return NotFound("Question with given QuestionID not found.");
            }

            var questionChoices = dbContext.QuestionChoices.Where(QuestionChoicesX => QuestionChoicesX.QuestionID == QuestionID && (QuestionChoicesX.IsActive && !QuestionChoicesX.IsDeleted));

            var questionChoicesResponse = questionChoices.Adapt<List<QuestionChoiceResponse>>();

            return Ok(questionChoicesResponse);
        }

        [HttpPost]
        [Route("{QuestionID:Guid}/choices")]
        public IActionResult CreateQuestionChoice(QuestionChoiceCreateRequest questionChoiceCreateRequest, [FromRoute] Guid QuestionID)
        {
            var newQuestionChoice = new QuestionChoice()
            {
                QuestionID = QuestionID,
                Text = questionChoiceCreateRequest.Text,
                Index = questionChoiceCreateRequest.Index,
                CreatorUserID = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)
            };

            dbContext.QuestionChoices.Add(newQuestionChoice);
            dbContext.SaveChanges();

            return Created("", newQuestionChoice.ID);
        }

        [HttpDelete]
        [Route("{QuestionID:Guid}/choices/{QuestionChoiceID:Guid}")]
        public IActionResult GetQuestionChoices([FromRoute] Guid QuestionID, [FromRoute] Guid QuestionChoiceID)
        {
            var question = dbContext.Questions.FirstOrDefault(QuestionX => QuestionX.ID == QuestionID && (QuestionX.IsActive && !QuestionX.IsDeleted));

            if (question == null)
            {
                return NotFound("Question with given QuestionID not found.");
            }

            var questionChoice = dbContext.QuestionChoices.FirstOrDefault(QuestionChoicesX => QuestionChoicesX.ID == QuestionChoiceID && (QuestionChoicesX.IsActive && !QuestionChoicesX.IsDeleted));

            if (questionChoice == null)
            {
                return NotFound("Question choice with given QuestionChoiceID not found.");
            }

            questionChoice.IsDeleted = true;
            dbContext.QuestionChoices.Update(questionChoice);
            dbContext.SaveChanges();

            return Ok();
        }

        [HttpPut]
        [Route("{QuestionID:Guid}")]
        public IActionResult UpdateQuestion([FromBody] QuestionUpdateRequest questionUpdateRequest, [FromRoute] Guid QuestionID)
        {
            var question = dbContext.Questions.FirstOrDefault(QuestionX => QuestionX.ID == QuestionID && (QuestionX.IsActive && !QuestionX.IsDeleted));

            if (question == null)
            {
                return NotFound("Question with given QuestionID not found.");
            }

            question.Text = questionUpdateRequest.Text ?? question.Text;
            question.Index = questionUpdateRequest.Index ?? question.Index;
            question.IsRequired = questionUpdateRequest.IsRequired ?? question.IsRequired;
            question.ModifiedAt = DateTime.Now;

            dbContext.Questions.Update(question);
            dbContext.SaveChanges();

            return Ok();
        }

        [HttpPut]
        [Route("{QuestionID:Guid}/active")]
        public IActionResult SetQuestionIsActive([FromRoute] Guid QuestionID, [FromBody] bool isActive)
        {
            var question = dbContext.Questions.FirstOrDefault(QuestionX => QuestionX.ID == QuestionID && (QuestionX.IsActive && !QuestionX.IsDeleted));

            if (question == null)
            {
                return NotFound("Question with given QuestionID not found.");
            }

            question.IsActive = isActive;
            question.ModifiedAt = DateTime.Now;
            dbContext.Questions.Update(question);
            dbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("{QuestionID:Guid}")]
        public IActionResult DeleteQuestion([FromRoute] Guid QuestionID)
        {
            var question = dbContext.Questions.FirstOrDefault(QuestionX => QuestionX.ID == QuestionID && (QuestionX.IsActive && !QuestionX.IsDeleted));

            if (question == null)
            {
                return NotFound("Question with given QuestionID not found.");
            }

            question.IsDeleted = true;
            question.ModifiedAt = DateTime.Now;
            dbContext.Questions.Update(question);
            dbContext.SaveChanges();

            return Ok();
        }

        [HttpPost]
        [Route("{QuestionID:Guid}/answers")]
        public IActionResult Answer([FromBody] AnswerRequest answerRequest, [FromRoute] Guid QuestionID)
        {
            var principal = HttpContext.User;
            var requestingID = new Guid(principal.FindFirst(ClaimTypes.NameIdentifier).Value);

            var answer = dbContext.Answers.FirstOrDefault(AnswerX => AnswerX.QuestionID == QuestionID && AnswerX.UserID == requestingID && (AnswerX.IsActive && !AnswerX.IsDeleted));

            if (answer != null)
            {
                answer.Text = answerRequest.Text;
                answer.QuestionChoiceID = answerRequest.QuestionChoiceID;
                answer.ModifiedAt = DateTime.Now;

                dbContext.Answers.Update(answer);
                dbContext.SaveChanges();

                return Ok();
            }
            else
            {
                var newAnswer = new Answer()
                {
                    QuestionID = QuestionID,
                    UserID = requestingID,
                    Text = answerRequest.Text,
                    QuestionChoiceID = answerRequest.QuestionChoiceID,
                    CreatorUserID = requestingID
                };

                dbContext.Answers.Add(newAnswer);
                dbContext.SaveChanges();

                return Created("", newAnswer.ID);
            }
        }

        [HttpPost]
        [Route("bulk/answers")]
        public IActionResult AnswerBulk([FromBody] AnswerBulkRequest answerRequest)
        {
            Console.WriteLine(answerRequest.QuestionIDs);
            Console.WriteLine(answerRequest.ChoicesData);

            var principal = HttpContext.User;
            var requestingID = new Guid(principal.FindFirst(ClaimTypes.NameIdentifier).Value);

            for (int i = 0; i < answerRequest.QuestionIDs.Length; i++)
            {
                var question = dbContext.Questions.FirstOrDefault(QuestionX => QuestionX.ID == answerRequest.QuestionIDs[i] && (QuestionX.IsActive && !QuestionX.IsDeleted));

                if (question == null)
                {
                    return NotFound("Question with given QuestionID not found.");
                }

                if (question.TypeID == 1 || question.TypeID == 2)
                {
                    var newAnswer = new Answer()
                    {
                        QuestionID = answerRequest.QuestionIDs[i],
                        UserID = requestingID,
                        Text = answerRequest.ChoicesData[i],
                        CreatorUserID = requestingID
                    };

                    dbContext.Answers.Add(newAnswer);
                }
                else if (question.TypeID == 3 || question.TypeID == 4)
                {
                    var newAnswer = new Answer()
                    {
                        QuestionID = answerRequest.QuestionIDs[i],
                        UserID = requestingID,
                        QuestionChoiceID = new Guid(answerRequest.ChoicesData[i]),
                        CreatorUserID = requestingID
                    };

                    dbContext.Answers.Add(newAnswer);
                }
            }

            dbContext.SaveChanges();

            return Ok();
        }

        [HttpGet]
        [Route("{QuestionID:Guid}/answers")]
        public IActionResult GetQuestionAnswers([FromRoute] Guid QuestionID)
        {
            var question = dbContext.Questions.FirstOrDefault(QuestionX => QuestionX.ID == QuestionID && (QuestionX.IsActive && !QuestionX.IsDeleted));

            if (question == null)
            {
                return NotFound("Question with given QuestionID not found.");
            }

            var questionAnswers = dbContext.Answers
                .Where(AnswerX => AnswerX.QuestionID == QuestionID)
                .Include(AnswerX => AnswerX.User)
                .Include(AnswerX => AnswerX.User.Role)
                .Include(AnswerX => AnswerX.QuestionChoice);

            var questionAnswersResponse = questionAnswers.Adapt<List<AnswerResponse>>();

            return Ok(questionAnswersResponse);
        }
    }
}
