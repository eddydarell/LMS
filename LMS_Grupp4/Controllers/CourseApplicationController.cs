using LMS_Grupp4.Models;
using LMS_Grupp4.Models.LMS_Models;
using LMS_Grupp4.Models.LMS_ViewModels;
using LMS_Grupp4.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS_Grupp4.Controllers
{
    [Authorize(Roles = "student, teacher")]
    public class CourseApplicationController : Controller
    {
        LMSRepository LMSRepo = new LMSRepository();

        //To-Do: CourseApplication Details and Delete.
        // GET: CourseApplication
        public ActionResult Index(int id = 0)
        {
            var userManager = LMSRepo.GetUserManager();
            bool isTeacher = userManager.IsInRole(User.Identity.GetUserId(), "teacher");
            List<CourseApplication> applications = null;

            if (isTeacher)//View content for the teacher
            {
                applications = LMSRepo.GetAllCourseApplications().Where(ca => ca.Course.ID == id).ToList();
                
            }
            else
            {
                applications = LMSRepo.GetAllCourseApplications().Where(ca => ca.Student.Id == User.Identity.GetUserId()).ToList();
            }

            //To-Do: 2 index view for teacher and student.
            return View(applications);
        }

        [Authorize(Roles = "student")]
        [HttpGet]
        public ActionResult Apply(int id = 0)
        {
            var userManager = LMSRepo.GetUserManager();
            var course = LMSRepo.GetCourseByID(id);
            var student = userManager.FindById(User.Identity.GetUserId());

            var model = new Course_ApplicationViewModel
            {
                CourseID = course.ID,
                CourseName = course.CourseName,
            };

            //If the student is already enrolled in a course
            if (course.Students.Contains(student))
            {
                return View("_ApplicationForbidden");
            }

            return View(model);
        }

        [Authorize(Roles = "student")]
        [HttpPost]
        public ActionResult Apply(int id = 0, string message = "")
        {
            var course = LMSRepo.GetCourseByID(id);
            var userManager = LMSRepo.GetUserManager();
            var roleManager = LMSRepo.GetRoleManager();
            var student = userManager.FindById(User.Identity.GetUserId());
            var users = userManager.Users.ToList();
            var teachers = new List<ApplicationUser>();

            //If the student is already enrolled in a course
            if (course.Students.Contains(student))
            {
                return View("_ApplicationForbidden");
            }

            //Filters only course teachers
            foreach (var teacher in users)
            {
                if (userManager.IsInRole(teacher.Id, "teacher") && teacher.Courses.Contains(course))
                {
                    teachers.Add(teacher);
                }
            }

            CourseApplication application = new CourseApplication
            {
                Message = message,
                EvaluationDate = null,
                IsAccepted = false,
                Course = course,
                ProgramClass = null,
                Student = student,
                Teachers = teachers,
                CreationDate = DateTime.Now,
                Status = false // Pending application by default
            };

            LMSRepo.AddCourseApplication(application);

            return RedirectToAction("Details", "Course", new { id = course.ID });
        }

        //To-Do: Test and evaluate this method
        [Authorize(Roles = "teacher")]
        [HttpGet]
        public ActionResult RespondToApplication(int id = 0)
        {
            var userManager = LMSRepo.GetUserManager();
            var application = LMSRepo.GetCourseApplicationID(id);

            var student = userManager.FindById(application.Student.Id);

            var model = new Course_ApplicationViewModel
            {
                CourseID = application.Course.ID,
                CourseName = application.Course.CourseName,
                Message = application.Message,
                StudentRealName = application.Student.RealName
            };

            return View(model);
        }

        [Authorize(Roles = "teacher")]
        [HttpPost]
        public ActionResult RespondToApplication(int id = 0, string comment = "", bool isAccepted = false)
        {
            var application = LMSRepo.GetCourseApplicationID(id);
            string tempMessage = "Your message:\n" + application.Message + "\nEnd Of Your Message" +
                "\n\nApplication evaluated by: " + User.Identity.GetUserRealName() + ".\nTeacher's Comment:\n";
            application.Message = tempMessage + comment + "\nEnd Of Comment.";

            application.IsAccepted = isAccepted;
            application.Status = true;
            application.EvaluationDate = DateTime.Now;

            var course = application.Course;
            var student = application.Student;

            //If the application is accepted, Add the student to the course
            if(isAccepted)
            {
                course.Students.Add(student);
                LMSRepo.EditCourse(course);
            }
            
            LMSRepo.EditCourseApplication(application);

            return RedirectToAction("Details", "Course", new { id = course.ID });
        }

        [HttpGet]
        public ActionResult Details(int id = 0)
        {
            var application = LMSRepo.GetCourseApplicationID(id);
            return View(application);
        }
    }
}