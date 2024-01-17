namespace TestApp.API.Models.Requests.Answer
{
    public class AnswerRequest
    {
        public string? Text { get; set; }

        public Guid? QuestionChoiceID { get; set; }
    }
}
