using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using LMS_Grupp4.Repositories;
using LMS_Grupp4.Models.LMS_ViewModels;
using LMS_Grupp4.Models.LMS_Models;

namespace LMS_Grupp4.Controllers
{
	public class AssignmentController : Controller
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
			var assignmentModelList = user.Assignments.ToList();

			return View(assignmentModelList);
		}

		[Authorize(Roles = "teacher")]
		[HttpGet]
		public ActionResult Create(int courseID = 0)
		{
			//var students = LMSRepo.GetRoleManager().FindByName("student").Users.ToList();
			var roleManager = LMSRepo.GetRoleManager();
			var studentRole = roleManager.FindByName("student");
			var course = LMSRepo.GetCourseByID(courseID);
			var students = course.Users.Where(stu => stu.Roles.FirstOrDefault(r => r.RoleId == studentRole.Id) != null).ToList();


			Assignment_CreateViewModel a_CVM = new Assignment_CreateViewModel(students);

			return View(a_CVM);
		}

		[Authorize(Roles = "teacher")]
		[HttpPost]
		public ActionResult Create([Bind(Include = "Name, DueDate, Percentage, MaxScore, Course, Student")] Assignment assignment)
		{
			return RedirectToAction("Index");
			//return View();
		}

		public ActionResult Details(int? id)
		{
			var assignment = LMSRepo.GetAssignmentByID(id);

			return View(assignment);
		}
	}
}