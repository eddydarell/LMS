namespace LMS_Grupp4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RevertedCoursesToHaveOneUserList : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Course_ID", "dbo.Courses");
            DropForeignKey("dbo.AspNetUsers", "Course_ID1", "dbo.Courses");
            DropIndex("dbo.Courses", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Course_ID" });
            DropIndex("dbo.AspNetUsers", new[] { "Course_ID1" });
            CreateTable(
                "dbo.ApplicationUserCourses",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Course_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Course_ID })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Course_ID, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Course_ID);
            
            DropColumn("dbo.Courses", "ApplicationUser_Id");
            DropColumn("dbo.AspNetUsers", "Course_ID");
            DropColumn("dbo.AspNetUsers", "Course_ID1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Course_ID1", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Course_ID", c => c.Int());
            AddColumn("dbo.Courses", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.ApplicationUserCourses", "Course_ID", "dbo.Courses");
            DropForeignKey("dbo.ApplicationUserCourses", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserCourses", new[] { "Course_ID" });
            DropIndex("dbo.ApplicationUserCourses", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserCourses");
            CreateIndex("dbo.AspNetUsers", "Course_ID1");
            CreateIndex("dbo.AspNetUsers", "Course_ID");
            CreateIndex("dbo.Courses", "ApplicationUser_Id");
            AddForeignKey("dbo.AspNetUsers", "Course_ID1", "dbo.Courses", "ID");
            AddForeignKey("dbo.AspNetUsers", "Course_ID", "dbo.Courses", "ID");
            AddForeignKey("dbo.Courses", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
