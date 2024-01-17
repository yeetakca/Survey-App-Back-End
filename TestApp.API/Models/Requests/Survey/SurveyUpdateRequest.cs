namespace TestApp.API.Models.Requests
{
    public class SurveyUpdateRequest
    {
        public int? TypeID { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }
    }
}