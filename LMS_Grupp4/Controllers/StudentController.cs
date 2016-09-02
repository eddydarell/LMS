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
		public ActionResult Index()
		{
			var user = LMSRepo.GetUserManager().FindById(User.Identity.GetUserId());
			var coursesWithPendingApplications = user.Courses.Where(c => c.CourseApplications.Any(ca => ca.Status == false)).ToList();

			List<CourseApplication> applications = new List<CourseApplication>();
			foreach (var course in coursesWithPendingApplications)
			{
				foreach (var application in course.CourseApplications)
				{
					if (!application.Status)
						applications.Add(application);
				}
			}

			var courseModel = user.Courses.ToList();

			List<Assignment> studentAssignments = new List<Assignment>();
			foreach (Assignment assignment in user.Assignments)
			{
				studentAssignments.Add(assignment);
			}
			
			List<IGrouping<string, LMSFile>> submissionFiles = new List<IGrouping<string, LMSFile>>();
			var files = LMSRepo.GetAllFiles();

			submissionFiles = files.Where(f => user.Courses.Contains(f.Course) && f.URL.Contains("Shared") || f.Uploader.Id == user.Id).OrderByDescending(f => f.UploadDate).Take(5).GroupBy(f => f.Course.CourseName).ToList();

			courseModel = courseModel.OrderByDescending(c => c.Assignments.Max(a => a.IssueDate) < DateTime.Now).Take(5).ToList();


			Student_IndexViewModel s_IVW = new Student_IndexViewModel(studentAssignments.OrderByDescending(a => a.IssueDate).Take(5).ToList(), courseModel, applications.OrderByDescending(a => a.CreationDate).Take(5).ToList(), submissionFiles);

			return View(s_IVW);
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

				Student_IndexViewModel stud_IVW = new Student_IndexViewModel(courseModelList, user);

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