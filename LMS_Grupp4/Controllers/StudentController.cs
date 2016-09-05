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

            //var studentAssignments = user.Courses.Select(c => c.Assignments.Where(a => !a.Evaluations.Any(e => e.Student.Id == user.Id))).ToList();
            List<Assignment> studentAssignments = new List<Assignment>();

            foreach (var course in user.Courses)
            {
                foreach(var assignment in course.Assignments)
                {
                    if(!assignment.Evaluations.Any(e => e.Student.Id == user.Id))
                    {
                        studentAssignments.Add(assignment);
                    }
                }

                //course.Assignments.Where(a => !a.Evaluations.Any(e => e.Student.Id == user.Id)).ToList();
            }

            //List<Assignment> studentAssignments = new List<Assignment>();
            //foreach (Assignment assignment in user.Assignments)
            //{
            //	studentAssignments.Add(assignment);
            //}

            List<IGrouping<string, LMSFile>> submissionFiles = new List<IGrouping<string, LMSFile>>();
			List<IGrouping<string, LMSFile>> courseFiles = new List<IGrouping<string, LMSFile>>();
			var files = LMSRepo.GetAllFiles();

			courseFiles = files.Where(f => user.Courses.Contains(f.Course) && f.URL.Contains("Shared") || f.Uploader.Id == user.Id && !f.URL.Contains("Submissions")).OrderByDescending(f => f.UploadDate).Take(5).GroupBy(f => f.Course.CourseName).ToList();
			submissionFiles = files.Where(f => user.Courses.Contains(f.Course) && f.URL.Contains("Submissions") && f.Uploader.Id == user.Id).OrderByDescending(f => f.UploadDate).Take(5).GroupBy(f => f.Course.CourseName).ToList();

			courseModel = courseModel.Take(5).ToList();
            
			Student_IndexViewModel s_IVW = new Student_IndexViewModel(studentAssignments.OrderBy(a => a.DueDate).Take(5).ToList(), courseModel, applications.OrderByDescending(a => a.CreationDate).Take(5).ToList(), submissionFiles, courseFiles);

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

        public ActionResult ComputeGrades()
        {
            var userManager = LMSRepo.GetUserManager();
            var user = userManager.FindById(User.Identity.GetUserId());

            List<IDictionary<string, double>> grades = new List<IDictionary<string, double>>();
            
            foreach (var course in user.Courses)
            {
                var courseDictionary = new Dictionary<string, double>();
                int assignmentCount = 0;
                double cumulPercentage = 0.00;

                foreach (var assignment in course.Assignments)
                {
                    assignmentCount++;
                    foreach (var eval in assignment.Evaluations)
                    {
                        if (eval.Student.Id == User.Identity.GetUserId())
                        {

                            if (eval.Percentage == null)
                            {
                                eval.Percentage = 0;
                                cumulPercentage += (double)eval.Percentage;
                            }
                            else
                            {
                                cumulPercentage += (double)eval.Percentage;
                            }
                        }
                    }
                }

                courseDictionary[course.CourseName] = Math.Round(cumulPercentage / assignmentCount);
                grades.Add(courseDictionary);
            }

            return PartialView("_GradesPartial", grades);
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