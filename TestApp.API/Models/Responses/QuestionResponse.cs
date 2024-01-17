using TestApp.API.Models.Domain;
using TestApp.API.Models.Domains;

namespace TestApp.API.Models.Responses
{
    public class QuestionResponse
    {
        public Guid ID { get; set; }

        public Guid SurveyID { get; set; }

        public QuestionTypeResponse Type { get; set; }

        public string Text { get; set; }

        public int Index { get; set; }

        public bool IsRequired { get; set; }
    }
}
