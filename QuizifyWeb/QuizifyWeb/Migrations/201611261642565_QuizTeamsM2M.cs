namespace QuizifyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuizTeamsM2M : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Teams", "Quiz_Id", "dbo.Quizs");
            DropIndex("dbo.Teams", new[] { "Quiz_Id" });
            CreateTable(
                "dbo.TeamQuizs",
                c => new
                    {
                        Team_Id = c.Int(nullable: false),
                        Quiz_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Team_Id, t.Quiz_Id })
                .ForeignKey("dbo.Teams", t => t.Team_Id, cascadeDelete: true)
                .ForeignKey("dbo.Quizs", t => t.Quiz_Id, cascadeDelete: true)
                .Index(t => t.Team_Id)
                .Index(t => t.Quiz_Id);
            
            DropColumn("dbo.Teams", "Quiz_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teams", "Quiz_Id", c => c.Int());
            DropForeignKey("dbo.TeamQuizs", "Quiz_Id", "dbo.Quizs");
            DropForeignKey("dbo.TeamQuizs", "Team_Id", "dbo.Teams");
            DropIndex("dbo.TeamQuizs", new[] { "Quiz_Id" });
            DropIndex("dbo.TeamQuizs", new[] { "Team_Id" });
            DropTable("dbo.TeamQuizs");
            CreateIndex("dbo.Teams", "Quiz_Id");
            AddForeignKey("dbo.Teams", "Quiz_Id", "dbo.Quizs", "Id");
        }
    }
}
