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
    public static class IdentityExtensions
    {
        public static string GetUserRealName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("RealName");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}