namespace LMS_Grupp4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditedAssignments : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Assignments", "Student_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Assignments", new[] { "Student_Id" });
            CreateTable(
                "dbo.ApplicationUserAssignments",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Assignment_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Assignment_ID })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Assignments", t => t.Assignment_ID, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Assignment_ID);
            
            DropColumn("dbo.Assignments", "Student_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Assignments", "Student_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.ApplicationUserAssignments", "Assignment_ID", "dbo.Assignments");
            DropForeignKey("dbo.ApplicationUserAssignments", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserAssignments", new[] { "Assignment_ID" });
            DropIndex("dbo.ApplicationUserAssignments", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserAssignments");
            CreateIndex("dbo.Assignments", "Student_Id");
            AddForeignKey("dbo.Assignments", "Student_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
