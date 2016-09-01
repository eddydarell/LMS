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
    [Authorize(Roles = "teacher")]
    public class TeacherController : Controller
    {
        LMSRepository LMSRepo = new LMSRepository();
        
        //public ActionResult Students(string id = "")
        //{
        //    var students = LMSRepo.GetRoleManager().FindByName("student").Users;
        //    var studentList = new List<ApplicationUser>();
        //    foreach (var student in students)
        //    {
        //        studentList.Add(LMSRepo.GetUserManager().FindById(student.UserId));
        //    }

        //    ViewBag.UserID = id;

        //    return View(studentList);
        //}

        public ActionResult Index()
        {
            var user = LMSRepo.GetUserManager().FindById(User.Identity.GetUserId());
            var coursesWithPendingApplications = user.Courses.Where(c => c.CourseApplications.Any(ca => ca.Status == false)).ToList();

            List<CourseApplication> applications = new List<CourseApplication>();
            foreach(var course in coursesWithPendingApplications)
            {
                foreach(var application in course.CourseApplications)
                {
                    if(!application.Status)
                        applications.Add(application);
                }
            }
            
            var courseModel = user.Courses.ToList();
            List<Assignment> teacherAssignments = new List<Assignment>();
            foreach (Course course in courseModel) 
            {
                foreach (Assignment assignment in course.Assignments) 
                {
                    teacherAssignments.Add(assignment);
                }
            }

            List<IGrouping<string, LMSFile>> submissionFiles = new List<IGrouping<string, LMSFile>>();
            var files = LMSRepo.GetAllFiles();
            submissionFiles = files.Where(f => user.Courses.Contains(f.Course) && f.URL.Contains("Submissions")).OrderByDescending(f => f.UploadDate).Take(3).GroupBy(f => f.Course.CourseName).ToList();

            Teacher_IndexViewModel teacher_IVW = new Teacher_IndexViewModel(teacherAssignments.OrderByDescending(a => a.IssueDate).Take(5).ToList(), courseModel.OrderByDescending(c => c.Users.Count).Take(5).ToList(), applications.OrderByDescending(a => a.CreationDate).Take(5).ToList(), submissionFiles);
            
            return View(teacher_IVW);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Details(string id = "")
        {
            var teacher = LMSRepo.GetUserManager().FindById(id);

            return View(teacher);
        }
    }
}