using TestApp.API.Models.Domain;

namespace TestApp.API.Models.Base
{
    public class BaseEntity
    {
        public Guid ID { get; set; } = Guid.NewGuid();

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Guid CreatorUserID { get; set; }
        public User CreatorUser { get; set; }

        public DateTime ModifiedAt { get; set; } = DateTime.Now;
    }
}
