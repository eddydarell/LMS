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
        LMSRepository LMSRepo = new LMSRepository();

        // GET: Admin
        public ActionResult Index(string id = "")
        {
            ViewBag.UserID = id;//To-Do: Delete this
            var roleManager = LMSRepo.GetRoleManager();
            var model = roleManager.Roles.ToList();
            return View(model);
        }

        public ActionResult ViewUsers()
        {
            var userManager = LMSRepo.GetUserManager();
            var model = userManager.Users.ToList();

            return View(model);
        }

        [HttpGet]
        public ActionResult Details(string id = "")
        {
            var userManager = LMSRepo.GetUserManager();
            var roleManager = LMSRepo.GetRoleManager();
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
            var userManager = LMSRepo.GetUserManager();
            var roleManager = LMSRepo.GetRoleManager();
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
            var userManager = LMSRepo.GetUserManager();
            var roleManager = LMSRepo.GetRoleManager();
            if (roleManager.RoleExists(role))
            {
                userManager.AddToRole(id, role);
            }
            
            return RedirectToAction("Details", new { id = id });
        }

        [HttpPost]
        public ActionResult RemoveUserRole(string id = "", string role = "")
        {
            var userManager = LMSRepo.GetUserManager();
            userManager.RemoveFromRole(id, role);

            return RedirectToAction("Details", new { id = id });
        }

        public ActionResult DeleteUser(string id = "")
        {
            var userManager = LMSRepo.GetUserManager();
            var user = userManager.FindById(id);
            userManager.Delete(user);

            return RedirectToAction("ViewUsers");
        }

        //Methods to manage roles(Adding and deleting)
        [HttpPost]
        public ActionResult AddRole(string roleName = "")
        {
            var roleManager = LMSRepo.GetRoleManager();
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
            var roleManager = LMSRepo.GetRoleManager();
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