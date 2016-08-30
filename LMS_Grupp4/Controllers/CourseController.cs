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
            var userManager = LMSRepo.GetUserManager();
            var courses = LMSRepo.GetAllCourses();

            if (userManager.IsInRole(User.Identity.GetUserId(), "student"))
            {
                var model = courses.Where(c => c.Users.SingleOrDefault(u => u.Id == User.Identity.GetUserId()) != null);

                return View("Index_Student", model);
            }
            else if(userManager.IsInRole(User.Identity.GetUserId(), "teacher"))
            {
                var model = courses.Where(c => c.Users.SingleOrDefault(u => u.Id == User.Identity.GetUserId()) != null);

                return View("Index", model); ;
            }
            else
            {
                return View(courses);
            }
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
            return View("Create_ViewModel");
        }

        [Authorize(Roles = "teacher")]
        [HttpPost]
        public ActionResult Create(Course_CreateViewModel course)
        {
            var userManager = LMSRepo.GetUserManager();
            var teacher = userManager.FindById(User.Identity.GetUserId());//Gets the actual user creating the course
            var model = new Course
            {
                CourseName = course.CourseName,
                Description = course.CourseDescription,
                CreationDate = DateTime.Now,
                Users = new List<ApplicationUser>(),

                //Important to declare these empty lists to get a 0 count in the view
                //The declared lists create relationships between the Course model and the others
                CourseApplications = new List<CourseApplication>(),
                Assignments = new List<Assignment>(),
                Classes = new List<ProgramClass>(),
                ClassSchema = new ClassSchema
                {
                    StartDate = course.ScheduleStartDate,
                    EndDate = course.ScheduleEndDate,
                    Schedule = course.editor,
                    Title = "Default Schedule"
                },
                Files = new List<LMSFile>()
            };

            model.Users.Add(teacher);
            
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

            Course_CreateViewModel courseViewModel = new Course_CreateViewModel
            {
                CourseID = model.ID,
                CourseDescription = model.Description,
                editor = model.ClassSchema.Schedule,
                CourseName = model.CourseName,
                ScheduleEndDate = model.ClassSchema.EndDate,
                ScheduleStartDate = model.ClassSchema.StartDate
            };

            var teacher = userManager.FindById(User.Identity.GetUserId());

            //security check
            //Verifies if the teacher attempting to edit the course is one of the owners of the course
            var courseTeachers = model.Users.Where(u => u.Roles.Where(r => r.RoleId == roleManager.FindByName("teacher").Id) != null);
            if(courseTeachers.Contains(teacher))
            {
                return View(courseViewModel);
            }
            else
            {
                return View("_Forbidden"); 
            }
        }

        [Authorize(Roles = "teacher")]
        [HttpPost]
        public ActionResult Edit(Course_CreateViewModel courseViewModel)
        {
            var userManager = LMSRepo.GetUserManager();
            var roleManager = LMSRepo.GetRoleManager();
            var course = LMSRepo.GetCourseByID(courseViewModel.CourseID);
            var teacher = userManager.FindById(User.Identity.GetUserId());

            //security check
            //Verifies if the teacher attempting to edit the course is one of the owners of the course
            var courseTeachers = course.Users.Where(u => u.Roles.Where(r => r.RoleId == roleManager.FindByName("teacher").Id) != null);
            if (courseTeachers.Contains(teacher))
            {
                if (ModelState.IsValid)
                {
                    if (!String.IsNullOrWhiteSpace(courseViewModel.CourseName))
                    {
                        course.CourseName = courseViewModel.CourseName;
                    }

                    if (!String.IsNullOrWhiteSpace(courseViewModel.CourseDescription))
                    {
                        course.Description = courseViewModel.CourseDescription;
                    }
                    if(!String.IsNullOrWhiteSpace(courseViewModel.editor))
                    {
                        course.ClassSchema.Schedule = courseViewModel.editor;
                    }

                    course.ClassSchema.EndDate = courseViewModel.ScheduleEndDate;
                    course.ClassSchema.StartDate = courseViewModel.ScheduleStartDate;

                    LMSRepo.EditCourse(course);

                    return RedirectToAction("Index");
                }
                return View(course);
            }
            else
            {
                return View("_Forbidden");
            }
        }

        [Authorize(Roles = "teacher")]
        [HttpGet]
        public ActionResult EnrollStudent(string studentID = "", int courseID = 0, string resultFormat = "")
        {
            var userManager = LMSRepo.GetUserManager();
            var student = userManager.FindById(studentID);
            var course = LMSRepo.GetCourseByID(courseID);

            course.Users.Add(student);
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
            var courseTeachers = course.Users.Where(u => u.Roles.Where(r => r.RoleId == roleManager.FindByName("teacher").Id) != null);
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

        public ActionResult FiveMostPopularCourses()
        {
            var model = LMSRepo.GetAllCourses().OrderByDescending(c => c.Users.Count).Take(5);

            return PartialView("_FiveMostPopularCourses", model);
        }
    }
}