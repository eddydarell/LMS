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


			if (!roleManager.RoleExists("admin"))
			{
				var role = new IdentityRole();
				role.Name = "admin";
				roleManager.Create(role);

				var user = new ApplicationUser();
				user.UserName = "admin@test.com";
				user.Email = "admin@test.com";
				user.RealName = "Administrator Administratorsson";

				string userPWD = "Admin@123";

				var chkUser = UserManager.Create(user, userPWD);

				if (chkUser.Succeeded)
				{
					var result1 = UserManager.AddToRole(user.Id, "admin");
				}
			}

			var student = UserManager.FindById("fe161dda-2802-41f0-8d88-97e7f617ec26");

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
				Description = "Basic Mathematics and introduction to counting",
				CreationDate = DateTime.Now
			};

			Course course2 = new Course
			{
				ID = 4,
				CourseName = "English 1",
				Assignments = new List<Assignment>(),
				Classes = new List<ProgramClass>(),
				ClassSchemes = new List<ClassSchema>(),
				Files = new List<LMSFile>(),
				Users = new List<ApplicationUser>(),
				Description = "Basic English",
				CreationDate = DateTime.Now
			};
			#endregion

			//#region Assignment Init
			//Assignment assignment1 = new Assignment
			//{
			//	ID = 1,
			//	MaxScore = 10,
			//	IsPassed = true,
			//	Name = "Math Quiz 1",
			//	DueDate = DateTime.Now.AddDays(7),
			//	IssueDate = DateTime.Now,
			//	Mark = "F",
			//	Course = course1,
			//	IsExpired = false
			//};
			//#endregion

			//context.Courses.Add(course1);
			//context.Assignments.Add(assignment1);
			course2.Users.Add(student);
			context.Courses.AddOrUpdate(course2);
			context.SaveChanges();
		}
	}
}
