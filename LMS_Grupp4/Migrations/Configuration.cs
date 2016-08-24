namespace LMS_Grupp4.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using Models.LMS_Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<LMS_Grupp4.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LMS_Grupp4.Models.ApplicationDbContext context)
        {
            Course course1 = new Course
            {
                ID = 1,
                Teachers = new List<ApplicationUser>(),
                CourseApplications = new List<CourseApplication>(),
                Assignments = new List<Assignment>(),
                Classes = new List<ProgramClass>(),
                CourseName = "JavaScript",
                CreationDate = DateTime.Now,
                Description = "Intro to JS",
                Files = new List<LMSFile>(),
                Students = new List<ApplicationUser>(),
                ClassSchema = new ClassSchema
                {
                    ID = 1,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(10),
                    Title = "JS Schedule"
                }
            };

            context.Courses.AddOrUpdate(course1);
            context.SaveChanges();
        }
    }
}

