using TestApp.API.Models.Domain;
using TestApp.API.Models.Domains;

namespace TestApp.API.Models.Responses
{
    public class AnswerResponse
    {
        public Guid QuestionID { get; set; }

        public UserResponse User { get; set; }

        public string? Text { get; set; }

        public QuestionChoiceResponse? QuestionChoice { get; set; }
    }
}
