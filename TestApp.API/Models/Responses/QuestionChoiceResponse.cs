using TestApp.API.Models.Domains;

namespace TestApp.API.Models.Responses
{
    public class QuestionChoiceResponse
    {
        public Guid ID { get; set; }

        public string Text { get; set; }

        public int Index { get; set; }
    }
}
