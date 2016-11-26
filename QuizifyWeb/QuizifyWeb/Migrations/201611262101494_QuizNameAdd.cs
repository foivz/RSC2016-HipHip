namespace QuizifyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuizNameAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quizs", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quizs", "Name");
        }
    }
}
