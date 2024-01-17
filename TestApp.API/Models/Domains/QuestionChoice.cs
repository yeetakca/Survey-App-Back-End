using TestApp.API.Models.Base;

namespace TestApp.API.Models.Domains
{
    public class QuestionChoice : BaseEntity
    {
        public Guid QuestionID { get; set; }
        public virtual Question Question { get; set; }

        public string Text { get; set; }

        public int Index { get; set; }
    }
}
