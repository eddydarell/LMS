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

		[Authorize(Roles = "teacher, student")]
		public ActionResult IndexUser(string userId = "")
		{
			if (String.IsNullOrWhiteSpace(userId))
			{
				userId = User.Identity.GetUserId();
			}
			ViewBag.UserID = userId;

			var user = LMSRepo.GetUserManager().FindById(userId);

			List<Assignment> assignmentModelList;

			if (User.IsInRole("student"))
			{
				try
				{
					assignmentModelList = user.Assignments.ToList();
					//Creating an empty list representing the teacherlist that is not necessary in Student user index,
					//but needed as parameter in the viewmodel.
					List<Assignment> emptyTeacherList = new List<Assignment>();
					Assignment_IndexUserViewModel A_IUVW = new Assignment_IndexUserViewModel(emptyTeacherList, assignmentModelList);

					return View(A_IUVW);
				} 
				catch (NullReferenceException)
				{
					return View();
				}
			} 
			else
			{
				var teacherCourses = user.Courses;
				List<Assignment> teacherAssignments = new List<Assignment>();
				List<Assignment> respondAssignments = new List<Assignment>();

				try
				{
					//Populates a list with all assignments a teacher got in his courses
					foreach (Course course in teacherCourses)
					{
						foreach (Assignment assignment in course.Assignments)
						{
							teacherAssignments.Add(assignment);
						}
					}
					//Populates a list with all assignments that has been confirmed by a student in 
					//any course that the teacher got.
					foreach (Course course in teacherCourses)
					{
						foreach (ApplicationUser courseUser in course.Users)
						{
							foreach(Assignment assignment in courseUser.Assignments)
							{
								if(!respondAssignments.Contains(assignment))
								{
									respondAssignments.Add(assignment);
								}
								
							}
						}
					}
					Assignment_IndexUserViewModel A_IUVW = new Assignment_IndexUserViewModel(teacherAssignments, respondAssignments);

					return View(A_IUVW);
				} 
				catch (NullReferenceException)
				{
					return View();
				}
			}
		}

		public ActionResult IndexCourse(int courseID = 0)
		{
			try 
			{
				Course course = LMSRepo.GetCourseByID(courseID);
				string courseName = course.CourseName;
				var courseAssignmentList = course.Assignments;

				Assignment_IndexCourseViewModel aICVM = new Assignment_IndexCourseViewModel(courseName, courseAssignmentList);

				return View(aICVM);
			}
			catch(NullReferenceException)
			{
				return RedirectToAction("IndexUser");
			}
		}

		[Authorize(Roles = "teacher")]
		[HttpGet]
		public ActionResult Create(int courseID = 0)
		{
			//Contains code for populating a drop-down list in the view, which is not used right now.
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
			assignment.IssueDate = DateTime.Now;
			//Connects the course to the assignment
			assignment.Course = course;
			assignment.Students = new List<ApplicationUser>();

			LMSRepo.AddAssignment(assignment);


			//var roleManager = LMSRepo.GetRoleManager();
			//var studentRole = roleManager.FindByName("student");
			//string id = User.Identity.GetUserId();
			//var teacher = LMSRepo.GetUserManager().FindById(id);

			//var students = course.Students.Where(stu => stu.Roles.FirstOrDefault(r => r.RoleId == studentRole.Id) != null).ToList();

			return RedirectToAction("IndexUser");
		}

		[Authorize(Roles = "teacher")]
		[HttpGet]
		public ActionResult Edit(int ID = 0)
		{
			Assignment assignment = LMSRepo.GetAssignmentByID(ID);

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

			return RedirectToAction("IndexUser");
		}

		[Authorize(Roles = "teacher")]
		[HttpGet]
		public ActionResult EditResponse(int ID = 0)
		{
			Assignment assignment = LMSRepo.GetAssignmentByID(ID);

			return View(assignment);
		}

		[Authorize(Roles = "teacher")]
		[HttpPost]
		public ActionResult EditResponse(int ID = 0, string Mark = "", bool IsPassed = false, int Score = 0)
		{
			//Assignment assignment = LMSRepo.GetAssignmentByID(ID);
			//assignment.Mark = Mark;
			//assignment.IsPassed = IsPassed;
			//assignment.Score = Score;
			//assignment.Percentage = (assignment.Score / assignment.MaxScore) * 100;

			//LMSRepo.EditAssignment(assignment);

			return RedirectToAction("IndexUser");
		}

		[Authorize(Roles = "student")]
		public ActionResult ConfirmAssignment(int id = 0)
		{
			string userId = User.Identity.GetUserId();
			var user = LMSRepo.GetUserManager().FindById(userId);

			Evaluation evaluation = new Evaluation();
			evaluation.Student = user;

			Assignment assignment = LMSRepo.GetAssignmentByID(id);
			assignment.Evaluations.Add(evaluation);

			user.Assignments.Add(assignment);

			LMSRepo.SaveChanges();

			return RedirectToAction("IndexUser");
		}

		public ActionResult Details(int? id)
		{
			var assignment = LMSRepo.GetAssignmentByID(id);

			return View(assignment);
		}

		[Authorize(Roles = "teacher")]
		public ActionResult Delete(int? id)
		{
			Assignment assignment = LMSRepo.GetAssignmentByID(id);

			return View(assignment);
		}

		[Authorize(Roles = "teacher")]
		public ActionResult DeleteConfirmed(int id)
		{
			LMSRepo.DeleteAssignment(id);
			
			return RedirectToAction("IndexUser");
		}
	}
}