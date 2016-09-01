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
	public class StudentController : Controller
	{
		LMSRepository LMSRepo = new LMSRepository();

		[Authorize(Roles = "student")]
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


		[Authorize(Roles = "student, teacher")]
		public ActionResult Details(string id = "")
		{
			var user = LMSRepo.GetUserManager().FindById(id);

			List<Course> courseModelList;
			List<Assignment> emptyFillerList = new List<Assignment>();

			try
			{
				courseModelList = user.Courses.ToList();

				Student_IndexViewModel stud_IVW = new Student_IndexViewModel(emptyFillerList, courseModelList);
				stud_IVW.User = user;

				return View(stud_IVW);
			} 
			catch(NullReferenceException e)
			{
				return RedirectToAction("ExceptionHandler", "Error");
			}
		}

		//public ActionResult Files(string id = "")
		//{
		//	var user = LMSRepo.GetUserManager().FindById(id);
		//	var filesModelList = user.Files.ToList();
		//	return View(filesModelList);
		//}

		//public ActionResult Assignments(string id = "")
		//{
		//	var user = LMSRepo.GetUserManager().FindById(id);
		//	var assignmentModelList = user.Assignments.ToList();
		//	return View(assignmentModelList);
		//}

		
	}
}