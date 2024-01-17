using TestApp.API.Models.Domains;

namespace TestApp.API.Models.Responses
{
    public class UserResponse
    {
        public Guid ID { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public RoleResponse Role { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
