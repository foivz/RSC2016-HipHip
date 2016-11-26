using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace QuizifyWeb.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
   


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<Arrival> Arrivals { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //za fluent api
        }

        //public DbSet<QuizifyWeb.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}