using TestApp.API.Models.Base;
using TestApp.API.Models.Domain;

namespace TestApp.API.Models.Domains
{
    public class Question : BaseEntity
    {
        public Guid SurveyID { get; set; }
        public virtual Survey Survey { get; set; }

        public int TypeID { get; set; }
        public virtual QuestionType Type { get; set; }

        public string Text { get; set; }

        public int Index { get; set; }

        public bool IsRequired { get; set; }
    }
}
