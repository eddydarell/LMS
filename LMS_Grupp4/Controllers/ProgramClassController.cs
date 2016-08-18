using LMS_Grupp4.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using LMS_Grupp4.Models.LMS_Models;

namespace LMS_Grupp4.Controllers
{
    [Authorize]
    public class ProgramClassController : Controller
    {
        LMSRepository LMSRepo = new LMSRepository();

        //To-Do: Add a well designed details view using a view model 
        // GET: ProgramClass
        public ActionResult Index()
        {
            var userManager = LMSRepo.GetUserManager();
            var user = userManager.FindById(User.Identity.GetUserId());
            var programClass = LMSRepo.GetAllProgramClasses();

            return View(programClass);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(string className = "")
        {
            if(ModelState.IsValid)
            {
                ProgramClass programClass = new ProgramClass
                {
                    ClassName = className
                };

                LMSRepo.AddProgramClass(programClass);

                return RedirectToAction("Index", "ProgramClass");
            }
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            var programClass = LMSRepo.GetProgramClassByID(id);
            return View(programClass);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(int id = 0, string className = "")
        {
            var programClass = LMSRepo.GetProgramClassByID(id);
            if(ModelState.IsValid)
            {
                programClass.ClassName = className;
                LMSRepo.EditProgramClass(programClass);

                return RedirectToAction("Index");
            }
            else
            {
                return View(programClass);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Delete(int id = 0)
        {
            LMSRepo.DeleteProgramClass(id);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Details(int id = 0)
        {
            var programClass = LMSRepo.GetProgramClassByID(id);

            return View(programClass);
        }

        //Adds a course ton a program
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult AddCourse(int id = 0)
        {
            var programClass = LMSRepo.GetProgramClassByID(id);
            return View(programClass);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult AddCourse(int courseID = 0, int programID = 0)
        {
            var programClass = LMSRepo.GetProgramClassByID(programID);
            var course = LMSRepo.GetCourseByID(courseID);

            programClass.Courses.Add(course);
            LMSRepo.EditProgramClass(programClass);

            return RedirectToAction("Details", new { id = programID });
        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult RemoveCourse(int courseID = 0, int programID = 0)
        {
            var programClass = LMSRepo.GetProgramClassByID(programID);
            var course = LMSRepo.GetCourseByID(courseID);

            programClass.Courses.Remove(course);
            LMSRepo.EditProgramClass(programClass);

            return RedirectToAction("Details", new { id = programID });
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult AddSchema(int programID = 0, int schemaID = 0)
        {
            var programClass = LMSRepo.GetProgramClassByID(programID);
            var classSchema = LMSRepo.GetClassSchemaByID(schemaID);

            programClass.ClassSchemas.Add(classSchema);
            LMSRepo.EditProgramClass(programClass);

            return RedirectToAction("Details", new { id = programID });
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult RemoveSchema(int programID = 0, int schemaID = 0)
        {
            var programClass = LMSRepo.GetProgramClassByID(programID);
            var classSchema = LMSRepo.GetClassSchemaByID(schemaID);

            programClass.ClassSchemas.Remove(classSchema);
            LMSRepo.EditProgramClass(programClass);

            return RedirectToAction("Details", new { id = programID });
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult AddUser(int id = 0)
        {
            var programClass = LMSRepo.GetProgramClassByID(id);
            return View(programClass);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult AddUser(int programID = 0, string userId = "")
        {
            var userManager = LMSRepo.GetUserManager();
            var programClass = LMSRepo.GetProgramClassByID(programID);
            var user = userManager.FindById(userId);

            programClass.Users.Add(user);
            LMSRepo.EditProgramClass(programClass);

            return RedirectToAction("Details", new { id = programID });
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult RemoveUser(int programID = 0, string userId = "")
        {
            var userManager = LMSRepo.GetUserManager();
            var programClass = LMSRepo.GetProgramClassByID(programID);
            var user = userManager.FindById(userId);

            programClass.Users.Remove(user);
            LMSRepo.EditProgramClass(programClass);

            return RedirectToAction("Details", new { id = programID });
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult RemoveAllUser(int programID = 0)
        {
            var programClass = LMSRepo.GetProgramClassByID(programID);
            programClass.Users.Clear();
            LMSRepo.EditProgramClass(programClass);

            return RedirectToAction("Details", new { id = programID });
        }
    }
}