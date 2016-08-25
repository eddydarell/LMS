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

        //    if (!roleManager.RoleExists("admin"))
        //    {
        //        var role = new IdentityRole();
        //        role.Name = "admin";
        //        roleManager.Create(role);

        //        var user = new ApplicationUser();
        //        user.UserName = "admin@test.com";
        //        user.Email = "admin@test.com";
        //        user.RealName = "Administrator Administratorsson";

        //        string userPWD = "Admin@123";

        //        var chkUser = UserManager.Create(user, userPWD);

        //        if (chkUser.Succeeded)
        //        {
        //            var result1 = UserManager.AddToRole(user.Id, "admin");
        //        }
        //    }

        //    #region Course Init
            //Course course1 = new Course
            //{
            //    ID = 1,
            //    CourseName = "Mathematics 1",
            //    Assignments = new List<Assignment>(),
            //    Classes = new List<ProgramClass>(),
            //    ClassSchema = { StartDate = DateTime.Now, EndDate = DateTime.Now },
            //    Files = new List<LMSFile>(),
            //    Users = new List<ApplicationUser>(),
            //    Description = "Basic Mathematics and introduction to counting",
            //    CreationDate = DateTime.Now
            //};

        //    Course course2 = new Course
        //    {
        //        ID = 2,
        //        CourseName = "Swedish",
        //        Assignments = new List<Assignment>(),
        //        Classes = new List<ProgramClass>(),
        //        ClassSchema = { StartDate = DateTime.Now, EndDate = DateTime.Now },
        //        Files = new List<LMSFile>(),
        //        Users = new List<ApplicationUser>(),
        //        Description = "Swedish ground level",
        //        CreationDate = DateTime.Now
        //    };

        //    Course course3 = new Course
        //    {
        //        ID = 3,
        //        CourseName = "Physics",
        //        Assignments = new List<Assignment>(),
        //        Classes = new List<ProgramClass>(),
        //        ClassSchema = { StartDate = DateTime.Now, EndDate = DateTime.Now },
        //        Files = new List<LMSFile>(),
        //        Users = new List<ApplicationUser>(),
        //        CreationDate = DateTime.Now
        //    };

        //    Course course4 = new Course
        //    {
        //        ID = 4,
        //        CourseName = "Chemistry",
        //        Assignments = new List<Assignment>(),
        //        Classes = new List<ProgramClass>(),
        //        ClassSchema = { StartDate = DateTime.Now, EndDate = DateTime.Now },
        //        Files = new List<LMSFile>(),
        //        Users = new List<ApplicationUser>(),
        //        CreationDate = DateTime.Now
        //    };
        //    #endregion

            //#region Assignment Init
            //Assignment assignment1 = new Assignment
            //{
            //    ID = 1,
            //    MaxScore = 10,
            //    IsPassed = true,
            //    Name = "Math Quiz 1",
            //    DueDate = DateTime.Now.AddDays(7),
            //    IssueDate = DateTime.Now,
            //    Mark = "F",
            //    Course = course1,
            //    IsExpired = false
            //};

            //Assignment assignment2 = new Assignment
            //{
            //    ID = 2,
            //    MaxScore = 10,
            //    IsPassed = true,
            //    Name = "Chemistry Quiz 1",
            //    DueDate = DateTime.Now.AddDays(7),
            //    IssueDate = DateTime.Now,
            //    Mark = "F",
            //    IsExpired = false,
            //    Course = course4
            //};
            //#endregion

        //    #region CourseApplication Init
        //    CourseApplication courseApplication1 = new CourseApplication
        //    {
        //        ID = 1,
        //        Message = "m1",
        //        Status = false,
        //        IsAccepted = false,
        //        CreationDate = DateTime.Now.AddDays(7),
        //        EvaluationDate = DateTime.Now,
        //    };

        //    CourseApplication courseApplication2 = new CourseApplication
        //    {
        //        ID = 2,
        //        Message = "m1",
        //        Status = false,
        //        IsAccepted = true,
        //        CreationDate = DateTime.Now.AddDays(7),
        //        EvaluationDate = DateTime.Now,
        //    };
        //    #endregion

        //    context.Courses.AddOrUpdate(course1);
        //    context.Courses.AddOrUpdate(course2);
        //    context.Courses.AddOrUpdate(course3);
        //    context.Courses.AddOrUpdate(course4);
        //    context.Assignments.AddOrUpdate(assignment1);
        //    context.Assignments.AddOrUpdate(assignment2);

            //var teacherUser = UserManager.FindById("c5559cf8-1b4b-4ff8-adcd-5329cee1e468");
        //    var teacherUser2 = UserManager.FindById("c9e8b510-b522-4742-acfe-4eefc94c92e4");

        //    teacherUser.Courses.Add(course1);
        //    teacherUser.Courses.Add(course2);
        //    teacherUser2.Courses.Add(course3);
        //    teacherUser2.Courses.Add(course4);
            //teacherUser.Assignments.Add(assignment1);
        //    teacherUser.Assignments.Add(assignment2);
        //    teacherUser.CourseApplications.Add(courseApplication2);

            //context.SaveChanges();
        }
    }
}