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
			} catch (NullReferenceException)
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
			var students = course.Users.Where(stu => stu.Roles.FirstOrDefault(r => r.RoleId == studentRole.Id) != null).ToList();


			Assignment_CreateViewModel a_CVM = new Assignment_CreateViewModel(students, courseID);

			return View(a_CVM);
		}

		[Authorize(Roles = "teacher")]
		[HttpPost]
		public ActionResult Create(int CourseID = 0, string Name = "", DateTime? DueDate = null, int MaxScore = 0)
		{
			Assignment assignment = new Assignment();
			Course course = LMSRepo.GetCourseByID(CourseID);

			assignment.Name = Name;
			assignment.DueDate = DueDate;
			assignment.MaxScore = MaxScore;
			assignment.DueDate = DueDate;
			assignment.IssueDate = DateTime.Now;
			//Connects the course to the assignment
			assignment.Course = course;
			assignment.Students = new List<ApplicationUser>();

			//Adding the assignment to the course
			//course.Assignments.Add(assignment);
			LMSRepo.AddAssignment(assignment);


			//var roleManager = LMSRepo.GetRoleManager();
			//var studentRole = roleManager.FindByName("student");
			//string id = User.Identity.GetUserId();
			//var teacher = LMSRepo.GetUserManager().FindById(id);

			//var students = course.Students.Where(stu => stu.Roles.FirstOrDefault(r => r.RoleId == studentRole.Id) != null).ToList();

			return RedirectToAction("Index");
		}

		[Authorize(Roles = "teacher")]
		[HttpGet]
		public ActionResult Edit(int assignmentID = 0)
		{
			Assignment assignment = LMSRepo.GetAssignmentByID(assignmentID);

			return View(assignment);
		}

		[Authorize(Roles = "teacher")]
		[HttpPost]
		public ActionResult Edit(int ID = 0, string Name = "", DateTime? DueDate = null)
		{
			Assignment assignment = LMSRepo.GetAssignmentByID(ID);
			assignment.Name = Name;
			assignment.DueDate = DueDate;

			LMSRepo.EditAssignment(assignment);

			return RedirectToAction("Index");
		}

		public ActionResult Details(int? id)
		{
			var assignment = LMSRepo.GetAssignmentByID(id);

			return View(assignment);
		}
	}
}