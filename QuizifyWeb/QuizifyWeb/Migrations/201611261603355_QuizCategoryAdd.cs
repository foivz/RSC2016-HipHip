namespace QuizifyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuizCategoryAdd : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Categories", newName: "QuestionCategories");
            RenameColumn(table: "dbo.Questions", name: "Category_Id", newName: "QuestionCategory_Id");
            RenameIndex(table: "dbo.Questions", name: "IX_Category_Id", newName: "IX_QuestionCategory_Id");
            CreateTable(
                "dbo.QuizCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Quizs", "QuizCategory_Id", c => c.Int());
            CreateIndex("dbo.Quizs", "QuizCategory_Id");
            AddForeignKey("dbo.Quizs", "QuizCategory_Id", "dbo.QuizCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Quizs", "QuizCategory_Id", "dbo.QuizCategories");
            DropIndex("dbo.Quizs", new[] { "QuizCategory_Id" });
            DropColumn("dbo.Quizs", "QuizCategory_Id");
            DropTable("dbo.QuizCategories");
            RenameIndex(table: "dbo.Questions", name: "IX_QuestionCategory_Id", newName: "IX_Category_Id");
            RenameColumn(table: "dbo.Questions", name: "QuestionCategory_Id", newName: "Category_Id");
            RenameTable(name: "dbo.QuestionCategories", newName: "Categories");
        }
    }
}
