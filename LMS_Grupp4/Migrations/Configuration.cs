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

   //     protected override void Seed(LMS_Grupp4.Models.ApplicationDbContext context)
   //     {
   //         RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context);
   //         RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);
   //         UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(context);
   //         UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(userStore);


   //         //At startup, creating a default admin role and a default admin user  
   //         if (!roleManager.RoleExists("admin"))
   //         {

   //             //// first we create Admin role   
   //             //var role = new IdentityRole();
   //             //role.Name = "admin";
   //             //roleManager.Create(role);

   //             ////Here we create a Admin super user who will maintain the website                  

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
}
