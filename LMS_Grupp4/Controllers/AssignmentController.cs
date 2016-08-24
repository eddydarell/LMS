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
using LMS_Grupp4.Models;

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

			List<Assignment> assignmentModelList;

			try
			{
				assignmentModelList = user.Assignments.ToList();
			}
			catch(NullReferenceException)
			{
				return View();
			}

			return View(assignmentModelList);
		}

		[Authorize(Roles = "teacher")]
		[HttpGet]
		public ActionResult Create(int courseID = 0)
		{
			var roleManager = LMSRepo.GetRoleManager();
			var studentRole = roleManager.FindByName("student");
			var course = LMSRepo.GetCourseByID(courseID);
			var students = course.Students.Where(stu => stu.Roles.FirstOrDefault(r => r.RoleId == studentRole.Id) != null).ToList();


			Assignment_CreateViewModel a_CVM = new Assignment_CreateViewModel(students, courseID);

			return View(a_CVM);
		}

		[Authorize(Roles = "teacher")]
		[HttpPost]
		public ActionResult Create(int CourseID = 0, string Name = "", DateTime? DueDate = null, int MaxScore = 0)
		{
			Course course = LMSRepo.GetCourseByID(CourseID);

			var roleManager = LMSRepo.GetRoleManager();
			var studentRole = roleManager.FindByName("student");
			string id = User.Identity.GetUserId();
			var teacher = LMSRepo.GetUserManager().FindById(id);

			var students = course.Students.Where(stu => stu.Roles.FirstOrDefault(r => r.RoleId == studentRole.Id) != null).ToList();

			foreach (ApplicationUser student in students)
			{
				Assignment assignment = new Assignment();
				assignment.Name = Name;
				assignment.DueDate = DueDate;
				assignment.MaxScore = MaxScore;
				assignment.Course = course;
				assignment.Students = new List<ApplicationUser>();
				assignment.IssueDate = DateTime.Now;
				student.Assignments.Add(assignment);
				//teacher.Assignments.Add(assignment);
				LMSRepo.AddAssignment(assignment);
			}

			return RedirectToAction("Index");
		}

		[Authorize(Roles = "teacher")]
		[HttpGet]
		public ActionResult Edit(int assignmentID = 0)
		{
			

			return View();
		}

		[Authorize(Roles = "teacher")]
		[HttpPost]
		public ActionResult Edit()
		{


			return View();
		}

		public ActionResult Details(int? id)
		{
			var assignment = LMSRepo.GetAssignmentByID(id);

			return View(assignment);
		}
	}
}