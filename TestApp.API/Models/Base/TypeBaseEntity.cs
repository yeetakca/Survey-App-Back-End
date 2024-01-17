namespace TestApp.API.Models.Base
{
    public class TypeBaseEntity
    {
        public int ID { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
