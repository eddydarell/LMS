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
        //static ApplicationDbContext context = new ApplicationDbContext();
        //static RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context);
        //RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);
        //static UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(context);
        //UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore);   

         // GET: /Student/Create

        //public ActionResult Create()
        //{
        //    return View();
        //} 

       //Get Student
        //public ActionResult Index(string id = "")
        //{
        //    var students = roleManager.FindByName("student").Users;
        //    var studentList = new List<ApplicationUser>();
        //    foreach(var student in students)
        //    {
        //        studentList.Add(userManager.FindById(student.UserId));
        //    }
        //    if (String.IsNullOrWhiteSpace(id))
        //    {
        //        id = User.Identity.GetUserId();
        //    }

        //    var user = userManager.FindById(id);
        //    var assignmentModel = user.Assignments.ToList();
        //    var courseModel = user.Courses.ToList();

        //   Teacher_IndexViewModel teacher_IVW = new Teacher_IndexViewModel(assignmentModel, courseModel, studentList);

        //    ViewBag.UserID = id;

        //    return View(teacher_IVW);
        //}

//        Get Student
        public ActionResult Students(string id = "")
        {
            var students = LMSRepo.GetRoleManager().FindByName("student").Users;           
            var studentList = new List<ApplicationUser>();
            foreach(var student in students)
            {
                studentList.Add(LMSRepo.GetUserManager().FindById(student.UserId));
            }           

            ViewBag.UserID = id;

            return View(studentList);
        }

        public ActionResult Index(string id = "")
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                id = User.Identity.GetUserId();
            }
            ViewBag.UserID = id;

            var user = LMSRepo.GetUserManager().FindById(id);
            var courseModel = user.Courses.ToList();
            var students = LMSRepo.GetUserManager().Users;

            Teacher_IndexViewModel teacher_IVW = new Teacher_IndexViewModel(courseModel, students);
            //teacher_IVW
           return View(courseModel);
        }

		public ActionResult Assignments(string id = "")
		{
            var user = LMSRepo.GetUserManager().FindById(id);
            var assignmentModelList = user.Assignments.ToList();
            return View(assignmentModelList);
		}

        ////Not needed at the moment
        //public ActionResult CourseApplication()
        //{
        //	var model = LMSRepo.GetAllCourses().ToList();

        //	return View(model);
        //}

        public ActionResult Courses(string id = "")
        {
            var user = LMSRepo.GetUserManager().FindById(id);
            var courseModelList = user.Courses.ToList();
            return View(courseModelList);
        }

        ////Not needed at the moment
        //public ActionResult ProgramClassApplication()
        //{
        //	var model = LMSRepo.GetAllProgramClasses().ToList();

        //	return View(model);
        //}

        //public ActionResult AddCourse(string id = "")
        //{	
        //    var user = userManager.FindById(id);
        //    return View(user);
        //}

        //[HttpPost]
        //public ActionResult AddCourse(string id = "", string courseName = "", string courseDescription = "")
        //{
        //    var user = userManager.FindById(id);
        //    var course = context.Courses.Find(id);

        //    Course tmpCourse = new Course();
        //    tmpCourse.CourseName = courseName;
        //    tmpCourse.Description = courseDescription;

        //    user.Courses.Add(tmpCourse);

        //    context.SaveChanges();

        //    return RedirectToAction("Index");

        //}

        //public ActionResult ClassSchemas(string id = "")
        //{
        //    var user = userManager.FindById(id);
        //    var model = user.ClassSchemas.ToList();

        //    return View(model);
        //}

        public ActionResult Files(string id = "")
		{
            var user = LMSRepo.GetUserManager().FindById(id);
            var filesModelList = user.Files.ToList();
            return View(filesModelList);
		}

        public ActionResult ProgramClasses(string id = "")
        {
            var user = LMSRepo.GetUserManager().FindById(id);
            var programClassModelList = user.ProgramClasses.ToList();
            return View(programClassModelList);
        }

    }
}