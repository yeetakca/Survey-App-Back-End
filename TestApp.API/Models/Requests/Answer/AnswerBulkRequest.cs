namespace TestApp.API.Models.Requests.Answer
{
    public class AnswerBulkRequest
    {
        public Guid[] QuestionIDs { get; set; }

        public string[] ChoicesData { get; set; }
    }
}
