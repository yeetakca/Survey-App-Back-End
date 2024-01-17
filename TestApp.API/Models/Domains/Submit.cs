using TestApp.API.Models.Base;
using TestApp.API.Models.Domain;

namespace TestApp.API.Models.Domains
{
    public class Submit : BaseEntity
    {
        public Guid SurveyID { get; set; }
        public Survey Survey { get; set; }

        public Guid UserID { get; set; }
        public User User { get; set; }
    }
}
