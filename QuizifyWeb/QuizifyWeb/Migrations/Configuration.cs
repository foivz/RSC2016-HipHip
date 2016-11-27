using QuizifyWeb.Models;

namespace QuizifyWeb.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<QuizifyWeb.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(QuizifyWeb.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //context.QuestionCategories.Add(new QuestionCategory
            //{
            //    Name = "A",
            //    Quiz = context.Quizzes.Find(4)
            //});

            //context.QuestionCategories.Add(new QuestionCategory
            //{
            //    Name = "B",
            //    Quiz = context.Quizzes.Find(4)
            //});


            //context.QuestionCategories.Add(new QuestionCategory
            //{
            //    Name = "C",
            //    Quiz = context.Quizzes.Find(4)
            //});

            //context.QuestionCategories.Add(new QuestionCategory
            //{
            //    Name = "D",
            //    Quiz = context.Quizzes.Find(4)
            //});

            //context.SaveChanges();

        }
    }
}
