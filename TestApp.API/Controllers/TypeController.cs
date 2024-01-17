using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApp.API.Data;

namespace TestApp.API.Controllers
{
    [Route("api/type")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly SurveyDbContext dbContext;

        public TypeController(IConfiguration configuration, SurveyDbContext dbContext)
        {
            _configuration = configuration;
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("survey")]
        public IActionResult GetSurveyTypes()
        {
            var types = dbContext.SurveyTypes.ToList();
            return Ok(types);
        }

        [HttpGet]
        [Route("question")]
        public IActionResult GetquestionTypes()
        {
            var types = dbContext.QuestionTypes.ToList();
            return Ok(types);
        }
    }
}
