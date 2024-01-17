using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.RegularExpressions;
using TestApp.API.Data;
using TestApp.API.Models.Domain;
using TestApp.API.Models.Requests;
using TestApp.API.Models.Requests.User;
using TestApp.API.Models.Responses;

namespace TestApp.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly SurveyDbContext dbContext;

        public UserController(IConfiguration configuration, SurveyDbContext dbContext)
        {
            _configuration = configuration;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = dbContext.Users.Where(UserX => (UserX.IsActive && !UserX.IsDeleted))
                .Include(UserX => UserX.Role);

            var usersResponse = users.Adapt<List<UserResponse>>();

            return Ok(usersResponse);
        }

        [HttpGet]
        [Route("{UserID:Guid}")]
        public IActionResult GetUser([FromRoute] Guid UserID)
        {
            var user = dbContext.Users
                .Include(UserX => UserX.Role)
                .FirstOrDefault(UserX => UserX.ID == UserID && (UserX.IsActive && !UserX.IsDeleted));

            if (user == null)
            {
                return NotFound("User with given UserID not found.");
            }

            var userResponse = user.Adapt<UserResponse>();

            return Ok(userResponse);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserCreateRequest userCreateRequest)
        {
            string emailPattern = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)*(\.[a-z]{2,7})$";
            if (!Regex.IsMatch(userCreateRequest.Email, emailPattern))
            {
                return BadRequest();
            }

            var newUser = new User()
            {
                Email = userCreateRequest.Email,
                Name = userCreateRequest.Name,
                Username= userCreateRequest.Username,
                Password = userCreateRequest.Password,
                RoleID = userCreateRequest.RoleID,
                CreatorUserID = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)
            };

            dbContext.Users.Add(newUser);
            dbContext.SaveChanges();

            return Created("", newUser.ID);
        }

        [HttpPut]
        [Route("{UserID:Guid}")]
        public IActionResult UpdateUser([FromBody] UserUpdateRequest userUpdateRequest, [FromRoute] Guid UserID)
        {
            var user = dbContext.Users.FirstOrDefault(UserX => UserX.ID == UserID && (UserX.IsActive && !UserX.IsDeleted));

            if (user == null)
            {
                return NotFound("User with given UserID not found.");
            }

            if (userUpdateRequest.Email != null)
            {
                string emailPattern = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)*(\.[a-z]{2,7})$";
                if (!Regex.IsMatch(userUpdateRequest.Email, emailPattern))
                {
                    return BadRequest();
                }

                user.Email = userUpdateRequest.Email;
            }
            
            user.Name = userUpdateRequest.Name ?? user.Name;
            user.Username = userUpdateRequest.Username ?? user.Username;
            user.Password = userUpdateRequest.Password ?? user.Password;
            user.RoleID = userUpdateRequest.RoleID ?? user.RoleID;
            user.ModifiedAt = DateTime.Now;

            dbContext.Users.Update(user);
            dbContext.SaveChanges();

            return Ok();
        }

        [HttpPut]
        [Route("{UserID:Guid}/active")]
        public IActionResult SetUserIsActive([FromRoute] Guid UserID, [FromBody] bool isActive)
        {
            var user = dbContext.Users
                .Include(UserX => UserX.Role)
                .FirstOrDefault(UserX => UserX.ID == UserID && (UserX.IsActive && !UserX.IsDeleted));

            if (user == null)
            {
                return NotFound("User with given UserID not found.");
            }

            user.IsActive = isActive;
            user.ModifiedAt = DateTime.Now;
            dbContext.Users.Update(user);
            dbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("{UserID:Guid}")]
        public IActionResult DeleteUser([FromRoute] Guid UserID)
        {
            var user = dbContext.Users
                .Include(UserX => UserX.Role)
                .FirstOrDefault(UserX => UserX.ID == UserID && (UserX.IsActive && !UserX.IsDeleted));

            if (user == null)
            {
                return NotFound("User with given UserID not found.");
            }

            user.IsDeleted = true;
            user.ModifiedAt = DateTime.Now;
            dbContext.Users.Update(user);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}