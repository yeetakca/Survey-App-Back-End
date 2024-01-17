using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TestApp.API.Models.Base;
using TestApp.API.Models.Domains;

namespace TestApp.API.Models.Domain
{
    public class Survey : BaseEntity
    {
        public int TypeID { get; set; }
        public virtual SurveyType Type { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }

    /*public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            //many-to-many relation definition for CustomerProduct mapping table  
            builder
            .HasMany(p => p.Users)
            .WithMany(p => p.Customers)
            .UsingEntity<UserCustomerMatch>(
                j => j
                    .HasOne(pt => pt.User)
                    .WithMany(t => t.UserCustomerMatches)
                    .HasForeignKey(pt => pt.UserId)
                    .OnDelete(DeleteBehavior.NoAction),
                j => j
                    .HasOne(pt => pt.Customer)
                    .WithMany(p => p.UserCustomerMatches)
                    .HasForeignKey(pt => pt.CustomerId)
                    .OnDelete(DeleteBehavior.NoAction));
        }
    }*/
}