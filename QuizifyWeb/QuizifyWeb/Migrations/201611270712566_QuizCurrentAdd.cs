namespace QuizifyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuizCurrentAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "IsCurrent", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "IsCurrent");
        }
    }
}
