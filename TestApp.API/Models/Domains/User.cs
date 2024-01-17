using TestApp.API.Models.Domains;
using TestApp.API.Models.Base;

namespace TestApp.API.Models.Domain
{
    public class User : UserBaseEntity
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int RoleID { get; set; }

        public virtual Role Role { get; set; }
    }
}