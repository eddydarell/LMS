namespace LMS_Grupp4.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using Models.LMS_Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LMS_Grupp4.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LMS_Grupp4.Models.ApplicationDbContext context)
        {
            RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context);
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);
            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(context);
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(userStore);


            //At startup, creating a default admin role and a default admin user  
            if (!roleManager.RoleExists("admin"))
            {

                //// first we create Admin role   
                //var role = new IdentityRole();
                //role.Name = "admin";
                //roleManager.Create(role);

                ////Here we create a Admin super user who will maintain the website                  

                //var user = new ApplicationUser();
                //user.UserName = "admin@admin.com";
                //user.Email = "admin@admin.com";
                //user.RealName = "Administrator";

                //string userPWD = "AdminPassword@123";

                //var chkUser = UserManager.Create(user, userPWD);

                ////Add default User to Role Admin   
                //if (chkUser.Succeeded)
                //{
                //    var result1 = UserManager.AddToRole(user.Id, "admin");
                //}
            }

            //var user = new ApplicationUser();
            //user.UserName = "teacher@test.com";
            //user.Email = "teacher@test.com";
            //user.RealName = "Tobias Keijser";

            //string userPWD = "Teacher@123";

            //var chkUser = UserManager.Create(user, userPWD);

            //if (chkUser.Succeeded)
            //{
            //    var result1 = UserManager.AddToRole(user.Id, "teacher");
            //}

            

            //var user = UserManager.FindById("bb073414-e2b2-4197-9f52-2a9535901341");

			var studentUser = UserManager.FindById("7023b4bb-2335-4729-8eed-b9566777ca49");
            //UserManager.AddToRole(user.Id, "teacher");

            //Creating Student role    
            //if (!roleManager.RoleExists("student"))
            //{
            //    var role = new IdentityRole();
            //    role.Name = "student";
            //    roleManager.Create(role);
            //}

            ////Creating Teacher role    
            //if (!roleManager.RoleExists("teacher"))
            //{
            //    var role = new IdentityRole();
            //    role.Name = "teacher";
            //    roleManager.Create(role);
            //}

            #region Course Init
            Course course1 = new Course
            {
                ID = 1,
                CourseName = "Mathematics 1",
                Assignments = new List<Assignment>(),
                Classes = new List<ProgramClass>(),
                ClassSchemes = new List<ClassSchema>(),
                Files = new List<LMSFile>(),
                Users = new List<ApplicationUser>(),
                Description = "Basic Mathematics and introduction to counting"
            };

            Course course2 = new Course
            {
                ID = 2,
                CourseName = "Swedish",
                Assignments = new List<Assignment>(),
                Classes = new List<ProgramClass>(),
                ClassSchemes = new List<ClassSchema>(),
                Files = new List<LMSFile>(),
                Users = new List<ApplicationUser>(),
                Description = "Swedish ground level"
            };

            Course course3 = new Course
            {
                ID = 3,
                CourseName = "Physics",
                Assignments = new List<Assignment>(),
                Classes = new List<ProgramClass>(),
                ClassSchemes = new List<ClassSchema>(),
                Files = new List<LMSFile>(),
                Users = new List<ApplicationUser>()
            };

            Course course4 = new Course
            {
                ID = 4,
                CourseName = "Chemistry",
                Assignments = new List<Assignment>(),
                Classes = new List<ProgramClass>(),
                ClassSchemes = new List<ClassSchema>(),
                Files = new List<LMSFile>(),
                Users = new List<ApplicationUser>()
            };
            #endregion

            #region ProgranClass Init
            ProgramClass programClass1 = new ProgramClass
            {
                ID = 1,
                ClassName = "IT Engineering",
                Courses = new List<Course>(),
                ApplicationUsers = new List<ApplicationUser>(),
                ClassSchemas = new List<ClassSchema>()
            };

            ProgramClass programClass2 = new ProgramClass
            {
                ID = 2,
                ClassName = "Mathematician",
                Courses = new List<Course>(),
                ApplicationUsers = new List<ApplicationUser>(),
                ClassSchemas = new List<ClassSchema>()
            };


            #endregion

            #region ClassShema Init
            ClassSchema classShema1 = new ClassSchema
            {
                ID = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(6),
                ProgramClass = programClass1,
                Courses = new List<Course>(),
                Teachers = new List<ApplicationUser>()
            };

            ClassSchema classShema2 = new ClassSchema
            {
                ID = 2,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(6),
                ProgramClass = programClass2,
                Courses = new List<Course>(),
                Teachers = new List<ApplicationUser>()
            };
            #endregion

            #region Assignment Init
            Assignment assignment1 = new Assignment
            {
                ID = 1,
                MaxScore = 10,
                IsPassed = true,
                Name = "Math Quiz 1",
                DueDate = DateTime.Now.AddDays(7),
                IssueDate = DateTime.Now,
                Mark = "F",
                Course = course1,
                IsExpired = false
            };

            Assignment assignment2 = new Assignment
            {
                ID = 2,
                MaxScore = 10,
                IsPassed = true,
                Name = "Chemistry Quiz 1",
                DueDate = DateTime.Now.AddDays(7),
                IssueDate = DateTime.Now,
                Mark = "F",
                IsExpired = false,
                Course = course4
            };
            #endregion

            classShema1.Courses.Add(course1);
            classShema1.Courses.Add(course2);
            classShema1.Courses.Add(course3);
            classShema1.Courses.Add(course4);
            classShema2.Courses.Add(course1);
            classShema2.Courses.Add(course2);

            context.Courses.AddOrUpdate(course1);
            context.Courses.AddOrUpdate(course2);
            context.Courses.AddOrUpdate(course3);
            context.Courses.AddOrUpdate(course4);
            context.Assignments.AddOrUpdate(assignment1);
            context.Assignments.AddOrUpdate(assignment2);
            context.ClassSchemas.AddOrUpdate(classShema1);
            context.ClassSchemas.AddOrUpdate(classShema2);
            context.ProgramClasses.AddOrUpdate(programClass1);
            context.ProgramClasses.AddOrUpdate(programClass2);

            context.SaveChanges();
        }
    }
}
