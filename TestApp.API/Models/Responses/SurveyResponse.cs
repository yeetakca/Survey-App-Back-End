using TestApp.API.Models.Domains;

namespace TestApp.API.Models.Responses
{
    public class SurveyResponse
    {
        public Guid ID { get; set; }

        public UserResponse CreatorUser { get; set; }

        public SurveyTypeResponse Type { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}