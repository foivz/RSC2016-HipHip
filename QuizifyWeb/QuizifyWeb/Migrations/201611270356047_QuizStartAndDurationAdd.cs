namespace QuizifyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuizStartAndDurationAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quizs", "DateTimeStarted", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.Quizs", "Duration", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.AspNetUsers", "IsPresent", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsPresent");
            DropColumn("dbo.Quizs", "Duration");
            DropColumn("dbo.Quizs", "DateTimeStarted");
        }
    }
}
