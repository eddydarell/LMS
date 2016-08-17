namespace LMS_Grupp4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AterAddingCourseApplicationModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseApplications",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Message = c.String(nullable: false, maxLength: 250),
                        Course_ID = c.Int(),
                        ProgramClass_ID = c.Int(),
                        Student_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Courses", t => t.Course_ID)
                .ForeignKey("dbo.ProgramClasses", t => t.ProgramClass_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.Student_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.Course_ID)
                .Index(t => t.ProgramClass_ID)
                .Index(t => t.Student_Id)
                .Index(t => t.ApplicationUser_Id);
            
            AddColumn("dbo.Courses", "CreationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "CourseApplication_ID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "CourseApplication_ID");
            AddForeignKey("dbo.AspNetUsers", "CourseApplication_ID", "dbo.CourseApplications", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseApplications", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "CourseApplication_ID", "dbo.CourseApplications");
            DropForeignKey("dbo.CourseApplications", "Student_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CourseApplications", "ProgramClass_ID", "dbo.ProgramClasses");
            DropForeignKey("dbo.CourseApplications", "Course_ID", "dbo.Courses");
            DropIndex("dbo.CourseApplications", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.CourseApplications", new[] { "Student_Id" });
            DropIndex("dbo.CourseApplications", new[] { "ProgramClass_ID" });
            DropIndex("dbo.CourseApplications", new[] { "Course_ID" });
            DropIndex("dbo.AspNetUsers", new[] { "CourseApplication_ID" });
            DropColumn("dbo.AspNetUsers", "CourseApplication_ID");
            DropColumn("dbo.Courses", "CreationDate");
            DropTable("dbo.CourseApplications");
        }
    }
}
