using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestApp.API.Data;
using TestApp.API.Models.Requests;

namespace TestApp.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly SurveyDbContext dbContext;

        public LoginController(IConfiguration configuration, SurveyDbContext dbContext) {
            _configuration = configuration;
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            var user = dbContext.Users.Include(UserX => UserX.Role)
                .FirstOrDefault(UserX => UserX.Username == loginRequest.Username && UserX.Password == loginRequest.Password && (UserX.IsActive && !UserX.IsDeleted));

            if (user == null)
            {
                return NotFound("User with given credentials not found.");
            }

            var role = dbContext.UserRoles.FirstOrDefault(RoleX => RoleX.ID == user.Role.ID);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                new Claim(ClaimTypes.Role, role.Name),
                new Claim(ClaimTypes.GroupSid, role.ID.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { Token = jwt });
        }

        [HttpPost]
        [Route("authCheck")]
        [Authorize]
        public IActionResult IsAuthenticated()
        {
            return Ok();
        }
    }
}