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
            #region User Init
            //User store and manager
            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(context);
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore);

            //Role store and manager
            RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context);
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);

            ApplicationUser student1 = new ApplicationUser
            {
                UserName = "student1@test.com",
                Email = "student1@test.com",
                RealName = "Markus Landberg",
                Assigments = new List<Assignment>(),
                ProgramClasses = new List<ProgramClass>(),
                Files = new List<File>(),
                Courses = new List<Course>()
            };

            ApplicationUser student2 = new ApplicationUser
            {
                UserName = "student2@test.com",
                Email = "student2@test.com",
                RealName = "Eddy Ntambwe",
                Assigments = new List<Assignment>(),
                ProgramClasses = new List<ProgramClass>(),
                Files = new List<File>(),
                Courses = new List<Course>()
            };

            ApplicationUser student3 = new ApplicationUser
            {
                UserName = "student3@test.com",
                Email = "student3@test.com",
                RealName = "Christian Wiren",
                Assigments = new List<Assignment>(),
                ProgramClasses = new List<ProgramClass>(),
                Files = new List<File>(),
                Courses = new List<Course>()
            };

            ApplicationUser teacher1 = new ApplicationUser
            {
                UserName = "teacher1@test.com",
                Email = "teacher1@test.com",
                RealName = "Tobias Keijser",
                Assigments = new List<Assignment>(),
                ProgramClasses = new List<ProgramClass>(),
                Files = new List<File>(),
                Courses = new List<Course>()
            };

            ApplicationUser teacher2 = new ApplicationUser
            {
                UserName = "teacher2@test.com",
                Email = "teacher2@test.com",
                RealName = "David Malmström",
                Assigments = new List<Assignment>(),
                ProgramClasses = new List<ProgramClass>(),
                Files = new List<File>(),
                Courses = new List<Course>()
            };


            #endregion

            #region Course Init
            Course course1 = new Course
            {
                ID = 1,
                CourseName = "Mathematics 1",
                Assignments = new List<Assignment>(),
                Classes = new List<ProgramClass>(),
                ClassSchemes = new List<ClassSchema>(),
                Files = new List<File>(),
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
                Files = new List<File>(),
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
                Files = new List<File>(),
                Users = new List<ApplicationUser>()
            };

            Course course4 = new Course
            {
                ID = 4,
                CourseName = "Chemistry",
                Assignments = new List<Assignment>(),
                Classes = new List<ProgramClass>(),
                ClassSchemes = new List<ClassSchema>(),
                Files = new List<File>(),
                Users = new List<ApplicationUser>()
            };

            context.Courses.AddOrUpdate(course1);
            context.Courses.AddOrUpdate(course2);
            context.Courses.AddOrUpdate(course3);
            context.Courses.AddOrUpdate(course4);
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
                ID = 1,
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
                ID = 1,
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

            Assignment assignment3 = new Assignment
            {
                ID = 3,
                Score = 10,
                MaxScore = 10,
                Percentage = 100.00,
                IsPassed = true,
                Name = "Swedish Quiz 1",
                DueDate = DateTime.Now,
                IssueDate = DateTime.Now,
                Mark = "A+",
                IsExpired = true,
                Course = course3,
                Student = student1
            };
            #endregion

            #region File Init
            File file1 = new File
            {
                ID = 1,
                Name = "wolf",
                Format = "jpg",
                Size = 151,
                UploadDate = DateTime.Now,
                URL = "Content/Uploads/Shared/",
                IsPublicVisible = true,
                Uploader = student2,
                Courses = new List<Course>()
            };
            #endregion

            #region Init
            programClass1.ApplicationUsers.Add(student1);
            programClass1.ApplicationUsers.Add(student2);
            programClass1.ApplicationUsers.Add(student3);
            programClass1.ApplicationUsers.Add(teacher1);
            programClass1.ApplicationUsers.Add(teacher2);
            programClass2.ApplicationUsers.Add(student1);
            programClass2.ApplicationUsers.Add(teacher1);
            programClass2.ApplicationUsers.Add(teacher1);

            file1.Courses.Add(course1);
            file1.Courses.Add(course2);

            classShema1.Courses.Add(course1);
            classShema1.Courses.Add(course2);
            classShema1.Courses.Add(course3);
            classShema1.Courses.Add(course4);
            classShema1.Teachers.Add(teacher1);
            classShema1.Teachers.Add(teacher2);

            classShema2.Courses.Add(course1);
            classShema2.Courses.Add(course2);
            classShema2.Teachers.Add(teacher2);



            context.Files.AddOrUpdate(file1);
            context.Assignments.AddOrUpdate(assignment1);
            context.Assignments.AddOrUpdate(assignment2);
            context.Assignments.AddOrUpdate(assignment3);
            context.ClassSchemas.AddOrUpdate(classShema1);
            context.ClassSchemas.AddOrUpdate(classShema2);
            context.ProgramClasses.AddOrUpdate(programClass1);
            context.ProgramClasses.AddOrUpdate(programClass2);

            student1.Courses.Add(course1);
            student1.Courses.Add(course2);
            student1.Courses.Add(course3);
            student1.Courses.Add(course4);

            student2.Courses.Add(course1);
            student2.Courses.Add(course2);
            student2.Courses.Add(course4);

            student3.Courses.Add(course1);
            student3.Courses.Add(course2);
            student3.Courses.Add(course4);

            teacher1.Courses.Add(course1);
            teacher1.Courses.Add(course2);
            teacher1.Courses.Add(course4);

            teacher2.Courses.Add(course3);

            var creationSuccessStudent1 = userManager.Create(student1, "Student@123");
            var creationSuccessStudent2 = userManager.Create(student2, "Student@123");
            var creationSuccessStudent3 = userManager.Create(student3, "Student@123");
            var creationSuccessTeacher1 = userManager.Create(teacher1, "Teacher@123");
            var creationSuccessTeacher2 = userManager.Create(teacher2, "Teacher@123");

            //Creating Student role    
            if (!roleManager.RoleExists("student"))
            {
                var role = new IdentityRole();
                role.Name = "student";
                roleManager.Create(role);

            }

            //Creating Teacher role    
            if (!roleManager.RoleExists("teacher"))
            {
                var role = new IdentityRole();
                role.Name = "teacher";
                roleManager.Create(role);
            }

            //To be added to a separate method
            //if (creationSuccessStudent1.Succeeded)
            //{
            //    userManager.AddToRole(student1.Id, "student");
            //}

            //if (creationSuccessStudent2.Succeeded)
            //{
            //    userManager.AddToRole(student2.Id, "student");
            //}

            //if (creationSuccessStudent3.Succeeded)
            //{
            //    userManager.AddToRole(student3.Id, "student");
            //}

            //if (creationSuccessTeacher1.Succeeded)
            //{
            //    userManager.AddToRole(teacher1.Id, "teacher");
            //}

            //if (creationSuccessTeacher2.Succeeded)
            //{
            //    userManager.AddToRole(teacher2.Id, "teacher");
            //}

            context.SaveChanges();
            #endregion
        }
    }
}
