using TestApp.API.Models.Domains;

namespace TestApp.API.Models.Requests.Question
{
    public class QuestionCreateRequest
    {
        public Guid SurveyID { get; set; }

        public int TypeID { get; set; }

        public string Text { get; set; }

        public int Index { get; set; }

        public bool IsRequired { get; set; }
    }
}
