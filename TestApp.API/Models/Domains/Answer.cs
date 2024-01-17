using TestApp.API.Models.Base;
using TestApp.API.Models.Domain;

namespace TestApp.API.Models.Domains
{
    public class Answer : BaseEntity
    {
        public Guid QuestionID { get; set; }
        public Question Question { get; set; }

        public Guid UserID { get; set; }
        public User User { get; set; }

        public string? Text { get; set; }
        
        public Guid? QuestionChoiceID { get; set; }
        public QuestionChoice? QuestionChoice { get; set; }
    }
}
