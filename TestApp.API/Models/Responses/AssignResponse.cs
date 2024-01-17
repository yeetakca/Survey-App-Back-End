using TestApp.API.Models.Domain;

namespace TestApp.API.Models.Responses
{
    public class AssignResponse
    {
        public Guid ID { get; set; }

        public User CreatorUser { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
