namespace LMS_Grupp4.Migrations
{
	using LMS_Grupp4.Models;
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using System;
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

			context.SaveChanges();
        }
    }
}
