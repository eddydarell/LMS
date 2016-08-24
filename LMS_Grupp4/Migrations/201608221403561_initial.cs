namespace LMS_Grupp4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assignments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        DueDate = c.DateTime(nullable: false),
                        IssueDate = c.DateTime(nullable: false),
                        Mark = c.String(maxLength: 2),
                        IsPassed = c.Boolean(nullable: false),
                        IsExpired = c.Boolean(nullable: false),
                        Score = c.Int(),
                        Percentage = c.Double(),
                        MaxScore = c.Int(nullable: false),
                        Course_ID = c.Int(),
                        Student_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Courses", t => t.Course_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.Student_Id)
                .Index(t => t.Course_ID)
                .Index(t => t.Student_Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CourseName = c.String(nullable: false),
                        Description = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProgramClasses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClassName = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CourseApplications",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Message = c.String(nullable: false, maxLength: 500),
                        Status = c.Boolean(nullable: false),
                        IsAccepted = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        EvaluationDate = c.DateTime(),
                        Course_ID = c.Int(),
                        ProgramClass_ID = c.Int(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        Student_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Courses", t => t.Course_ID)
                .ForeignKey("dbo.ProgramClasses", t => t.ProgramClass_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Student_Id)
                .Index(t => t.Course_ID)
                .Index(t => t.ProgramClass_ID)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Student_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        RealName = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        CourseApplication_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CourseApplications", t => t.CourseApplication_ID)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.CourseApplication_ID);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ClassSchemas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        ProgramClass_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProgramClasses", t => t.ProgramClass_ID)
                .Index(t => t.ProgramClass_ID);
            
            CreateTable(
                "dbo.LMSFiles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Format = c.String(maxLength: 20),
                        URL = c.String(nullable: false),
                        IsPublicVisible = c.Boolean(nullable: false),
                        UploadDate = c.DateTime(nullable: false),
                        Size = c.Double(nullable: false),
                        Assignment_ID = c.Int(),
                        Uploader_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Assignments", t => t.Assignment_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.Uploader_Id)
                .Index(t => t.Assignment_ID)
                .Index(t => t.Uploader_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.ClassSchemaCourses",
                c => new
                    {
                        ClassSchema_ID = c.Int(nullable: false),
                        Course_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ClassSchema_ID, t.Course_ID })
                .ForeignKey("dbo.ClassSchemas", t => t.ClassSchema_ID, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Course_ID, cascadeDelete: true)
                .Index(t => t.ClassSchema_ID)
                .Index(t => t.Course_ID);
            
            CreateTable(
                "dbo.ClassSchemaApplicationUsers",
                c => new
                    {
                        ClassSchema_ID = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ClassSchema_ID, t.ApplicationUser_Id })
                .ForeignKey("dbo.ClassSchemas", t => t.ClassSchema_ID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.ClassSchema_ID)
                .Index(t => t.ApplicationUser_Id);
            
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
            
            CreateTable(
                "dbo.LMSFileCourses",
                c => new
                    {
                        LMSFile_ID = c.Int(nullable: false),
                        Course_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LMSFile_ID, t.Course_ID })
                .ForeignKey("dbo.LMSFiles", t => t.LMSFile_ID, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Course_ID, cascadeDelete: true)
                .Index(t => t.LMSFile_ID)
                .Index(t => t.Course_ID);
            
            CreateTable(
                "dbo.ApplicationUserProgramClasses",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        ProgramClass_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.ProgramClass_ID })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.ProgramClasses", t => t.ProgramClass_ID, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.ProgramClass_ID);
            
            CreateTable(
                "dbo.ProgramClassCourses",
                c => new
                    {
                        ProgramClass_ID = c.Int(nullable: false),
                        Course_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProgramClass_ID, t.Course_ID })
                .ForeignKey("dbo.ProgramClasses", t => t.ProgramClass_ID, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Course_ID, cascadeDelete: true)
                .Index(t => t.ProgramClass_ID)
                .Index(t => t.Course_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ProgramClassCourses", "Course_ID", "dbo.Courses");
            DropForeignKey("dbo.ProgramClassCourses", "ProgramClass_ID", "dbo.ProgramClasses");
            DropForeignKey("dbo.AspNetUsers", "CourseApplication_ID", "dbo.CourseApplications");
            DropForeignKey("dbo.CourseApplications", "Student_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserProgramClasses", "ProgramClass_ID", "dbo.ProgramClasses");
            DropForeignKey("dbo.ApplicationUserProgramClasses", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.LMSFiles", "Uploader_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.LMSFileCourses", "Course_ID", "dbo.Courses");
            DropForeignKey("dbo.LMSFileCourses", "LMSFile_ID", "dbo.LMSFiles");
            DropForeignKey("dbo.LMSFiles", "Assignment_ID", "dbo.Assignments");
            DropForeignKey("dbo.ApplicationUserCourses", "Course_ID", "dbo.Courses");
            DropForeignKey("dbo.ApplicationUserCourses", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CourseApplications", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ClassSchemaApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ClassSchemaApplicationUsers", "ClassSchema_ID", "dbo.ClassSchemas");
            DropForeignKey("dbo.ClassSchemas", "ProgramClass_ID", "dbo.ProgramClasses");
            DropForeignKey("dbo.ClassSchemaCourses", "Course_ID", "dbo.Courses");
            DropForeignKey("dbo.ClassSchemaCourses", "ClassSchema_ID", "dbo.ClassSchemas");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Assignments", "Student_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CourseApplications", "ProgramClass_ID", "dbo.ProgramClasses");
            DropForeignKey("dbo.CourseApplications", "Course_ID", "dbo.Courses");
            DropForeignKey("dbo.Assignments", "Course_ID", "dbo.Courses");
            DropIndex("dbo.ProgramClassCourses", new[] { "Course_ID" });
            DropIndex("dbo.ProgramClassCourses", new[] { "ProgramClass_ID" });
            DropIndex("dbo.ApplicationUserProgramClasses", new[] { "ProgramClass_ID" });
            DropIndex("dbo.ApplicationUserProgramClasses", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.LMSFileCourses", new[] { "Course_ID" });
            DropIndex("dbo.LMSFileCourses", new[] { "LMSFile_ID" });
            DropIndex("dbo.ApplicationUserCourses", new[] { "Course_ID" });
            DropIndex("dbo.ApplicationUserCourses", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ClassSchemaApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ClassSchemaApplicationUsers", new[] { "ClassSchema_ID" });
            DropIndex("dbo.ClassSchemaCourses", new[] { "Course_ID" });
            DropIndex("dbo.ClassSchemaCourses", new[] { "ClassSchema_ID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.LMSFiles", new[] { "Uploader_Id" });
            DropIndex("dbo.LMSFiles", new[] { "Assignment_ID" });
            DropIndex("dbo.ClassSchemas", new[] { "ProgramClass_ID" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "CourseApplication_ID" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.CourseApplications", new[] { "Student_Id" });
            DropIndex("dbo.CourseApplications", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.CourseApplications", new[] { "ProgramClass_ID" });
            DropIndex("dbo.CourseApplications", new[] { "Course_ID" });
            DropIndex("dbo.Assignments", new[] { "Student_Id" });
            DropIndex("dbo.Assignments", new[] { "Course_ID" });
            DropTable("dbo.ProgramClassCourses");
            DropTable("dbo.ApplicationUserProgramClasses");
            DropTable("dbo.LMSFileCourses");
            DropTable("dbo.ApplicationUserCourses");
            DropTable("dbo.ClassSchemaApplicationUsers");
            DropTable("dbo.ClassSchemaCourses");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.LMSFiles");
            DropTable("dbo.ClassSchemas");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.CourseApplications");
            DropTable("dbo.ProgramClasses");
            DropTable("dbo.Courses");
            DropTable("dbo.Assignments");
        }
    }
}
