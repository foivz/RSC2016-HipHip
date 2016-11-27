namespace QuizifyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuizIsPreparedAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quizs", "IsPrepared", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quizs", "IsPrepared");
        }
    }
}
