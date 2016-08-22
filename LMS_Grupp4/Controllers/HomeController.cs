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
        public ActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
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