using LMS_Grupp4.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LMS_Grupp4.Startup))]
namespace LMS_Grupp4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
           //createRolesandUsers();
        }

        //Method to create the roles for the application  
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            //At startup, creating a default admin role and a default admin user  
            if (!roleManager.RoleExists("admin"))
            {

                // first we create Admin role   
                var role = new IdentityRole();
                role.Name = "admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser();
                user.UserName = "admin@admin.com";
                user.Email = "admin@admin.com";

                string userPWD = "AdminPassword@123";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "admin");
                }
            }

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
        }
    }
}
