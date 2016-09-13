using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections;
using LMS_Grupp4.Models.LMS_Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Security.Principal;

namespace LMS_Grupp4.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        //To-Do: Add the other properties
        [Display(Name = "Name")]
        public string RealName { get; set; }
        public virtual ICollection<CourseApplication> CourseApplications { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
		public virtual ICollection<ClassSchema> ClassSchemas { get; set; }
        public virtual ICollection<LMSFile> Files { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<ProgramClass> ProgramClasses { get; set; }
		public virtual ICollection<Evaluation> Evaluations { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("RealName", RealName.ToString()));
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<ClassSchema> ClassSchemas { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<LMSFile> Files { get; set; }
        public DbSet<ProgramClass> ProgramClasses { get; set; }
        public DbSet<CourseApplication> CourseApplications { get; set; }
		public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<ScheduleEvent> Events { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    //This class in intended to provide extensions to the Use.Identity object
    public static class PersonalIdentityExtensions
    {
        public static string GetUserRealName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("RealName");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }

        //Extension function
        public static string SetMark(double percentage)
        {
            string mark = "";

            if (percentage >= 90)
            {
                mark = "A";
            }
            else if (percentage < 90 && percentage >= 80)
            {
                mark = "B";
            }
            else if (percentage < 80 && percentage >= 70)
            {
                mark = "C";
            }
            else if (percentage < 70 && percentage >= 60)
            {
                mark = "D";
            }
            else if (percentage < 60 && percentage >= 50)
            {
                mark = "E";
            }
            else if (percentage < 50 && percentage >= 40)
            {
                mark = "Fx";
            }
            else
            {
                mark = "F";
            }

            return mark;
        }

        public static string DisplayEventPeriodicity(int periodicity = 0)
        {
            switch(periodicity)
            {
                case 0: return "None";
                case 1: return "Daily";
                case 2: return "Weekly";
                case 3: return "Monthly";
                case 4: return "Yearly";
            }

            return "Not Specified.";
        }

        public static string DisplayEventPriority(int priority = 0)
        {
            switch(priority)
            {
                case 1: return "Normal";
                case 2: return "Low";
                case 3: return "High";
            }

            return "Normal";
        }

        public static string DisplayEventNature(int nature = 0)
        {
            switch(nature)
            {
                case 1: return "Lecture";
                case 2: return "Test";
                case 3: return "Exam";
                case 4: return "Lab";
                case 5: return "Cancellation";
                case 6: return "Other";
            }

            return "Other";
        }
    }
}