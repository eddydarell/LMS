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
    [Authorize]
    public class CourseController : Controller
    {
        LMSRepository LMSRepo = new LMSRepository();

        // GET: Course
        public ActionResult Index()
        {
            var courses = LMSRepo.GetAllCourses();
            return View(courses);
        }

        public ActionResult Details(int id = 0)
        {
            var model = LMSRepo.GetCourseByID(id);
            var userManager = LMSRepo.GetUserManager();
            if(userManager.IsInRole(User.Identity.GetUserId(), "teacher"))
            {
                return View("Details_Teacher", model);
            }
            else
            {
                return View("Details_Student", model);
            }
        }

        [Authorize(Roles = "teacher")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "teacher")]
        [HttpPost]
        public ActionResult Create(string CourseName = "", string description = "")
        {
            var userManager = LMSRepo.GetUserManager();
            var teacher = userManager.FindById(User.Identity.GetUserId());//Gets the actual user creating the course
            var model = new Course
            {
                CourseName = CourseName,
                Description = description,
                CreationDate = DateTime.Now,
                Students = new List<ApplicationUser>(),
                Teachers = new List<ApplicationUser>(),

                //Important to declare these empty lists to get a 0 count in the view
                //The declared lists create relationships between the Course model and the others
                CourseApplications = new List<CourseApplication>(),
                Assignments = new List<Assignment>(),
                Classes = new List<ProgramClass>(),
                ClassSchema = new ClassSchema
                {
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddMonths(6)
                },
                Files = new List<LMSFile>()
            };

            model.Teachers.Add(teacher);
            
            if(ModelState.IsValid)
            {
                LMSRepo.AddCourse(model);

                return RedirectToAction("Index");
            }
            
            return View(model);
        }

        [Authorize(Roles = "teacher")]
        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            var userManager = LMSRepo.GetUserManager();
            var roleManager = LMSRepo.GetRoleManager();
            var model = LMSRepo.GetCourseByID(id);
            var teacher = userManager.FindById(User.Identity.GetUserId());

            //security check
            //Verifies if the teacher attempting to edit the course is one of the owners of the course
            var courseTeachers = model.Teachers.Where(u => u.Roles.Where(r => r.RoleId == roleManager.FindByName("teacher").Id) != null);
            if(courseTeachers.Contains(teacher))
            {
                return View(model);
            }
            else
            {
                return View("_Forbidden"); //To-Do: Proper handling
            }
        }

        [Authorize(Roles = "teacher")]
        [HttpPost]
        public ActionResult Edit(int id = 0, string courseName = "", string description = "")
        {
            var userManager = LMSRepo.GetUserManager();
            var roleManager = LMSRepo.GetRoleManager();
            var course = LMSRepo.GetCourseByID(id);
            var teacher = userManager.FindById(User.Identity.GetUserId());

            //security check
            //Verifies if the teacher attempting to edit the course is one of the owners of the course
            var courseTeachers = course.Teachers.Where(u => u.Roles.Where(r => r.RoleId == roleManager.FindByName("teacher").Id) != null);
            if (courseTeachers.Contains(teacher))
            {
                if (ModelState.IsValid)
                {
                    if (!String.IsNullOrWhiteSpace(courseName))
                    {
                        course.CourseName = courseName;
                    }

                    if (!String.IsNullOrWhiteSpace(description))
                    {
                        course.Description = description;
                    }

                    LMSRepo.EditCourse(course);

                    return RedirectToAction("Index");
                }
                return View(course);
            }
            else
            {
                return View("_Forbidden"); //To-Do: Proper handling
            }
        }

        [Authorize(Roles = "teacher")]
        [HttpGet]
        public ActionResult EnrollStudent(string studentID = "", int courseID = 0, string resultFormat = "")
        {
            var userManager = LMSRepo.GetUserManager();
            var student = userManager.FindById(studentID);
            var course = LMSRepo.GetCourseByID(courseID);

            course.Students.Add(student);
            LMSRepo.EditCourse(course);

            if(resultFormat == "json")
            {
                return Json(new { status = "Success"}, JsonRequestBehavior.AllowGet);
            }
            return View();
        }

        [Authorize(Roles = "teacher")]
        [HttpGet]
        public ActionResult Delete(int id = 0)
        {
            var userManager = LMSRepo.GetUserManager();
            var roleManager = LMSRepo.GetRoleManager();
            var course = LMSRepo.GetCourseByID(id);
            var teacher = userManager.FindById(User.Identity.GetUserId());

            //security check
            //Verifies if the teacher attempting to edit the course is one of the owners of the course
            var courseTeachers = course.Teachers.Where(u => u.Roles.Where(r => r.RoleId == roleManager.FindByName("teacher").Id) != null);
            if (courseTeachers.Contains(teacher))
            {
                LMSRepo.DeleteCourse(id);

                return RedirectToAction("Index");
            }
            else
            {
                return View("_Forbidden");
            }   
        }
    }
}