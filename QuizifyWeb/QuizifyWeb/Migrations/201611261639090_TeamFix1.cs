namespace QuizifyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeamFix1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Team_Id", "dbo.Teams");
            DropIndex("dbo.AspNetUsers", new[] { "Team_Id" });
            CreateTable(
                "dbo.TeamApplicationUsers",
                c => new
                    {
                        Team_Id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Team_Id, t.ApplicationUser_Id })
                .ForeignKey("dbo.Teams", t => t.Team_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.Team_Id)
                .Index(t => t.ApplicationUser_Id);
            
            DropColumn("dbo.AspNetUsers", "Team_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Team_Id", c => c.Int());
            DropForeignKey("dbo.TeamApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TeamApplicationUsers", "Team_Id", "dbo.Teams");
            DropIndex("dbo.TeamApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.TeamApplicationUsers", new[] { "Team_Id" });
            DropTable("dbo.TeamApplicationUsers");
            CreateIndex("dbo.AspNetUsers", "Team_Id");
            AddForeignKey("dbo.AspNetUsers", "Team_Id", "dbo.Teams", "Id");
        }
    }
}
