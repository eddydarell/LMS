using LMS_Grupp4.Models;
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
	[Authorize(Roles = "student")]
    public class StudentController : Controller
    {
		static ApplicationDbContext context = new ApplicationDbContext();
		static RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context);
		RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);
		static UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(context);
		UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore);

        // GET: Student
		public ActionResult Index(string id = "")
        {
			if(String.IsNullOrWhiteSpace(id))
			{
				id = User.Identity.GetUserId();
			}

			ViewBag.UserID = id;
            return View();
        }

		public ActionResult Courses(string id = "")
		{
			var user = userManager.FindById(id);
			var model = user.Courses.ToList();
			return View(model);
		}

		public ActionResult AddCourse(string id = "")
		{
			return View();
		}

		[HttpPost]
		public ActionResult AddCourse()
		{
			return View();
		}

		public ActionResult ProgramClasses(string id = "")
		{
			var user = userManager.FindById(id);
			var model = user.ProgramClasses.ToList();

			return View(model);
		}
    }
}