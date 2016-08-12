using LMS_Grupp4.Models;
using LMS_Grupp4.Models.LMS_ViewModels;
using LMS_Grupp4.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS_Grupp4.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        static ApplicationDbContext context = new ApplicationDbContext();
        static RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context);
        RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);
        static UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(context);
        UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore);

        // GET: Admin
        public ActionResult Index(string id = "")
        {
            ViewBag.UserID = id;
            return View();
        }

        public ActionResult ViewUsers()
        {
            var model = userManager.Users.ToList();

            return View(model);
        }

        [HttpGet]
        public ActionResult Details(string id = "")
        {
            var user = userManager.FindById(id);
            var userRoles = new List<string>();
            foreach(var role in user.Roles)
            {
                userRoles.Add(roleManager.FindById(role.RoleId).Name);
            }

            var model = new Admin_ManageUserViewModel
            {
                UserEmail = user.Email, UserID = user.Id, Username = user.UserName, UserPhoneNumber = user.PhoneNumber, UserRealName = user.RealName, UserRoles = userRoles
            };
            return View(model);
        }

        public ActionResult AddRole()
        {
            return View();
        }
    }
}