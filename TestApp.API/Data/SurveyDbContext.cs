using Microsoft.EntityFrameworkCore;
using TestApp.API.Models.Domain;
using TestApp.API.Models.Domains;

namespace TestApp.API.Data
{
    public class SurveyDbContext: DbContext
    {
        public SurveyDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Survey> Surveys { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<QuestionChoice> QuestionChoices { get; set; }

        public DbSet<Role> UserRoles { get; set; }

        public DbSet<SurveyType> SurveyTypes { get; set; }

        public DbSet<QuestionType> QuestionTypes { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<Submit> Submits { get; set; }

        public DbSet<Assign> Assigns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //new CustomerConfiguration().Configure(modelBuilder.Entity<Customer>());

            modelBuilder.Entity<Role>().HasData(
                new Role() { ID = 1, Name = "end user"},
                new Role() { ID = 2, Name = "admin"},
                new Role() { ID = 3, Name = "root"}
            );
            modelBuilder.Entity<SurveyType>().HasData(
                new SurveyType() { ID = 1, Name = "single page" },
                new SurveyType() { ID = 2, Name = "multi page" }
            );
            modelBuilder.Entity<QuestionType>().HasData(
                new QuestionType() { ID = 1, Name = "text" },
                new QuestionType() { ID = 2, Name = "true/false" },
                new QuestionType() { ID = 3, Name = "sa multiple choice" },
                new QuestionType() { ID = 4, Name = "ma multiple choice" }
            );
            modelBuilder.Entity<User>().HasData(
                new User() { Email = "", Name = "root", Username = "root", Password = "root", RoleID = 3 }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}