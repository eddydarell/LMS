using LMS_Grupp4.Models;
using LMS_Grupp4.Models.LMS_Models;
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
	[Authorize(Roles = "student")]
    public class StudentController : Controller
    {
		static ApplicationDbContext context = new ApplicationDbContext();
		static RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context);
		RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);
		static UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(context);
		UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore);

		LMSRepository LMSRepo = new LMSRepository();

        // GET: Student
		public ActionResult Index(string id = "")
        {
			if(String.IsNullOrWhiteSpace(id))
			{
				id = User.Identity.GetUserId();
			}
			ViewBag.UserID = id;

			var user = userManager.FindById(id);
			var assignmentModel = user.Assignments.ToList();
			var courseModel = user.Courses.ToList();
			
			Student_IndexViewModel stud_IVW = new Student_IndexViewModel(assignmentModel, courseModel);

            return View(stud_IVW);
        }

		public ActionResult Files(string id = "")
		{
			var user = userManager.FindById(id);
			var model = user.Files.ToList();
			return View(model);
		}

		public ActionResult Assignments(string id = "")
		{
			var user = userManager.FindById(id);
			var model = user.Assignments.ToList();
			return View(model);
		}

		////Not needed at the moment
		//public ActionResult ApplyToCourse()
		//{
		//	var model = LMSRepo.GetAllCourses().ToList();

		//	return View(model);
		//}

		public ActionResult ProgramClasses(string id = "")
		{
			var user = userManager.FindById(id);
			var model = user.ProgramClasses.ToList();

			return View(model);
		}

		////Not needed at the moment
		//public ActionResult ApplyToProgramClass()
		//{
		//	var model = LMSRepo.GetAllProgramClasses().ToList();

		//	return View(model);
		//}

		public ActionResult ClassSchemas(string id = "")
		{
			var user = userManager.FindById(id);
			var model = user.ClassSchemas.ToList();

			return View(model);
		}

    }
}