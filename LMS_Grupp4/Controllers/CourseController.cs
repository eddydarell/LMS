using LMS_Grupp4.Models;
using LMS_Grupp4.Models.LMS_Models;
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
        private static ApplicationDbContext context = new ApplicationDbContext();
        static RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context);
        RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);
        static UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(context);
        UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore);

        // GET: Course
        public ActionResult Index()
        {
            var courses = context.Courses.ToList();
            return View(courses);
        }

        public ActionResult Details(int id = 0)
        {
            var model = context.Courses.Find(id);
            return View(model);
        }

        [Authorize(Roles = "student")]
        [HttpGet]
        public ActionResult Apply(int id = 0)
        {
            var course = context.Courses.Find(id);
            var student = userManager.FindById(User.Identity.GetUserId());

            //If the student is already enrolled in a course
            if(course.Users.Contains(student))
            {
                return View("_ApplicationForbidden");
            }

            return View(course);
        }

        [Authorize(Roles = "student")]
        [HttpPost]
        public ActionResult Apply()
        {
            return null;//To-Do: Implement this method
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
            var teacher = userManager.FindById(User.Identity.GetUserId());//Gets the actual user creating the course
            var model = new Course
            {
                CourseName = CourseName,
                Description = description,
                CreationDate = DateTime.Now,
                Users = new List<ApplicationUser>(),

                //Important to declare these empty lists to get a 0 count in the view
                //The declared lists create relationships between the Course model and the others
                Assignments = new List<Assignment>(),
                Classes = new List<ProgramClass>(),
                ClassSchemes = new List<ClassSchema>(),
                Files = new List<LMSFile>(),
            };

            model.Users.Add(teacher);
            
            if(ModelState.IsValid)
            {
                context.Courses.Add(model);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            
            return View(model);
        }

        [Authorize(Roles = "teacher")]
        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            var model = context.Courses.Find(id);
            var teacher = userManager.FindById(User.Identity.GetUserId());

            //security check
            //Verifies if the teacher attempting to edit the course is one of the owners of the course
            var courseTeachers = model.Users.Where(u => u.Roles.Where(r => r.RoleId == roleManager.FindByName("teacher").Id) != null);
            if(courseTeachers.Contains(teacher))
            {
                return View(model);
            }
            else
            {
                return View("EditForbidden"); //To-Do: Proper handling
            }
        }

        [Authorize(Roles = "teacher")]
        [HttpPost]
        public ActionResult Edit(int id = 0, string courseName = "", string description = "")
        {
            var course = context.Courses.Find(id);
            var teacher = userManager.FindById(User.Identity.GetUserId());

            //security check
            //Verifies if the teacher attempting to edit the course is one of the owners of the course
            var courseTeachers = course.Users.Where(u => u.Roles.Where(r => r.RoleId == roleManager.FindByName("teacher").Id) != null);
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

                    context.SaveChanges();

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
        public ActionResult Delete(int id = 0)
        {
            var course = context.Courses.Find(id);
            var teacher = userManager.FindById(User.Identity.GetUserId());

            //security check
            //Verifies if the teacher attempting to edit the course is one of the owners of the course
            var courseTeachers = course.Users.Where(u => u.Roles.Where(r => r.RoleId == roleManager.FindByName("teacher").Id) != null);
            if (courseTeachers.Contains(teacher))
            {
                context.Courses.Remove(course);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View("_Forbidden");
            }   
        }
    }
}