namespace QuizifyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuizonqAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "Quiz_Id", c => c.Int());
            CreateIndex("dbo.Questions", "Quiz_Id");
            AddForeignKey("dbo.Questions", "Quiz_Id", "dbo.Quizs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "Quiz_Id", "dbo.Quizs");
            DropIndex("dbo.Questions", new[] { "Quiz_Id" });
            DropColumn("dbo.Questions", "Quiz_Id");
        }
    }
}
