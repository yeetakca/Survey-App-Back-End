namespace TestApp.API.Models.Requests.Question
{
    public class QuestionUpdateRequest
    {
        public string? Text { get; set; }

        public int? Index { get; set; }

        public bool? IsRequired { get; set; }
    }
}
