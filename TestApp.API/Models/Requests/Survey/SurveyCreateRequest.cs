namespace TestApp.API.Models.Requests.Survey
{
    public class SurveyCreateRequest
    {
        public int TypeID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
