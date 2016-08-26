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
		LMSRepository LMSRepo = new LMSRepository();

		public ActionResult Index(string id = "")
		{
			if (String.IsNullOrWhiteSpace(id))
			{
				id = User.Identity.GetUserId();
			}
			ViewBag.UserID = id;

			var user = LMSRepo.GetUserManager().FindById(id);

			List<Assignment> assignmentModelList;
			List<Course> courseModelList;

			try
			{
				//List<Assignment> assignmentModelList = new List<Assignment>();
				assignmentModelList = user.Assignments.ToList();
				//List<Course> courseModelList = new List<Course>();
				courseModelList = user.Courses.ToList();
			} catch (NullReferenceException)
			{
				return View();
			}

			Student_IndexViewModel stud_IVW = new Student_IndexViewModel(assignmentModelList, courseModelList);

			return View(stud_IVW);
		}

		public ActionResult Files(string id = "")
		{
			var user = LMSRepo.GetUserManager().FindById(id);
			var filesModelList = user.Files.ToList();
			return View(filesModelList);
		}

		public ActionResult Assignments(string id = "")
		{
			var user = LMSRepo.GetUserManager().FindById(id);
			var assignmentModelList = user.Assignments.ToList();
			return View(assignmentModelList);
		}

		////Not needed at the moment
		//public ActionResult ApplyToCourse()
		//{
		//	var model = LMSRepo.GetAllCourses().ToList();

		//	return View(model);
		//}

		public ActionResult ProgramClasses(string id = "")
		{
			var user = LMSRepo.GetUserManager().FindById(id);
			var programClassModelList = user.ProgramClasses.ToList();

			return View(programClassModelList);
		}

		////Not needed at the moment
		//public ActionResult ApplyToProgramClass()
		//{
		//	var model = LMSRepo.GetAllProgramClasses().ToList();

		//	return View(model);
		//}

		public ActionResult ClassSchemas(string id = "")
		{
			var user = LMSRepo.GetUserManager().FindById(id);
			var classSchemaModelList = user.ClassSchemas.ToList();

			return View(classSchemaModelList);
		}
	}
}