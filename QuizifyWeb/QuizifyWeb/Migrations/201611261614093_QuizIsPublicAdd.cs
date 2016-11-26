namespace QuizifyWeb.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class QuizIsPublicAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quizs", "IsPublic", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quizs", "IsPublic");
        }
    }
}
