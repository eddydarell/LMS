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
            ViewBag.UserID = id;//To-Do: Delete this
            var model = roleManager.Roles.ToList();
            return View(model);
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

        //Manages the roles for a given user by id
        [HttpGet]
        public ActionResult ManageUserRoles(string id = "")
        {
            var user = userManager.FindById(id);
            var userRoles = new List<string>();
            foreach (var role in user.Roles)
            {
                userRoles.Add(roleManager.FindById(role.RoleId).Name);
            }

            var model = new Admin_ManageUserViewModel
            {
                UserEmail = user.Email,
                UserID = user.Id,
                Username = user.UserName,
                UserPhoneNumber = user.PhoneNumber,
                UserRealName = user.RealName,
                UserRoles = userRoles
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AddUserRole(string id = "", string role = "")
        {
            if(roleManager.RoleExists(role))
            {
                userManager.AddToRole(id, role);
            }
            
            return RedirectToAction("Details", new { id = id });
        }

        [HttpPost]
        public ActionResult RemoveUserRole(string id = "", string role = "")
        {
            userManager.RemoveFromRole(id, role);
            return RedirectToAction("Details", new { id = id });
        }

        public ActionResult DeleteUser(string id = "")
        {
            var user = userManager.FindById(id);
            userManager.Delete(user);
            return RedirectToAction("ViewUsers");
        }

        //Methods to manage roles(Adding and deleting)
        [HttpPost]
        public ActionResult AddRole(string roleName = "")
        {
            if (!String.IsNullOrWhiteSpace(roleName))
            {
                IdentityRole role = new IdentityRole
                {
                    Name = roleName.ToLower()
                };
                roleManager.Create(role);
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteRole(string id = "")
        {
            var role = roleManager.FindById(id);
            roleManager.Delete(role);
            return RedirectToAction("Index"); ;
        }

        public ActionResult RoleDetails(string id = "")
        {
            //To-Do: Finish this method. Create new viewmodel for this
            return View();
        }
    }
}