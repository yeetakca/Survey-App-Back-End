using TestApp.API.Models.Domains;

namespace TestApp.API.Models.Requests
{
    public class UserCreateRequest
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int RoleID { get; set; }
    }
}
