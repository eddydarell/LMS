using LMS_Grupp4.Models.LMS_ViewModels;
using LMS_Grupp4.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS_Grupp4.Controllers
{
    public class HomeController : Controller
    {
        LMSRepository LMSRepo = new LMSRepository();

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var isAdmin = User.IsInRole("admin");
                var isTeacher = User.IsInRole("teacher");
                var isStudent = User.IsInRole("student");
                if (isAdmin)
                {
                    return RedirectToActionPermanent("Index", "Admin", new { id = User.Identity.GetUserId() });
                }

                if (isTeacher)
                {
                    return RedirectToActionPermanent("Index", "Teacher", new { id = User.Identity.GetUserId() });
                }

                if (isStudent)
                {
                    return RedirectToActionPermanent("Index", "Student", new { id = User.Identity.GetUserId() });
                }
            }
            return View();
        }

        public ActionResult Facts()
        {
            Home_IndexViewModel facts = new Home_IndexViewModel();
            var userManager = LMSRepo.GetUserManager();
            var roleManager = LMSRepo.GetRoleManager();
            var teacherRoleID = roleManager.FindByName("teacher").Id;
            var studentRoleID = roleManager.FindByName("student").Id;

            facts.CourseCount = LMSRepo.GetAllCourses().Count();
            facts.TeachersCount = userManager.Users.Where(u => u.Roles.Where(r => r.RoleId == teacherRoleID).Count() > 0).ToList().Count;
            facts.StudentsCount = userManager.Users.Where(u => u.Roles.Where(r => r.RoleId == studentRoleID).Count() > 0).ToList().Count;

            int mostStudents = LMSRepo.GetAllCourses().Max(c => c.Users.Count);
            facts.MostPopularCourse = LMSRepo.GetAllCourses().FirstOrDefault(c => c.Users.Count == mostStudents);

            return PartialView("_FactsPartial", facts);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}