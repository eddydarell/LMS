using LMS_Grupp4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS_Grupp4.Controllers
{
    public class CourseController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: Course
        public ActionResult Index()
        {
            var courses = context.Courses.ToList();
            return View(courses);
        }
    }
}