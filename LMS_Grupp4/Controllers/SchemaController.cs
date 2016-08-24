using LMS_Grupp4.Models;
using LMS_Grupp4.Models.LMS_Models;
using LMS_Grupp4.Repositories;
using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS_Grupp4.Controllers
{
    //This controller is STRICTLY linked to a ProgramClass Instance. 
    //Delete MUST BE cascading
    [Authorize(Roles = "admin")]
    public class SchemaController : Controller
    {
        LMSRepository LMSRepo = new LMSRepository();

        // GET: Schema
        public ActionResult Index(int id = 0)
        {
            if(id <= 0)//If no ProgramClass Identifier is sent, display error
            {
                return null; //To-Do: Proper handling
            }
            var programClass = LMSRepo.GetProgramClassByID(id);
            var schemas = programClass.ClassSchemas;

            return View(schemas);
        }

        // GET: Schema/Details/5
        public ActionResult Details(int programID = 0, int id = 0)
        {
            if (programID <= 0)//If no ProgramClass Identifier is sent, display error
            {
                return null; //To-Do: Proper handling
            }
            var programClass = LMSRepo.GetProgramClassByID(id);
            var schema = programClass.ClassSchemas.SingleOrDefault(s => s.ID == id);

            return View(schema);
        }

        // GET: Schema/Create
        [HttpGet]
        public ActionResult Create(int programID = 0)
        {
            var programClass = LMSRepo.GetProgramClassByID(programID);
            if(programID <= 0)
            {
                return null;
            }
            ViewBag.ProgramID = programID;//Sends the program id and name along to the view
            ViewBag.ProgramName = programClass.ClassName;

            return View();
        }

        // POST: Schema/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            //To-Do: Try...Catch on all the db action on all controllers
            try
            {
                DateTime startDate = Convert.ToDateTime(collection["StartDate"]);
                DateTime endDate = Convert.ToDateTime(collection["EndDate"]);
                int programID = Int32.Parse(collection["programID"]);

                var roleManage = LMSRepo.GetRoleManager();
                var teacherRoleID = roleManage.FindByName("teacher").Id;
                
                var programClass = LMSRepo.GetProgramClassByID(programID);
                List<Course> courses = programClass.Courses.ToList();
                List<ApplicationUser> teachers = programClass.Users.Where(u => u.Roles.FirstOrDefault(r => r.RoleId == teacherRoleID) != null).ToList();

                //By default all courses and teachers for schema come from the ProgramClass
                //ClassSchema classSchema = new ClassSchema
                //{
                //    Title = "Schema for: " + programClass.ClassName + " From: " + startDate + "To" + endDate,
                //    StartDate = startDate,
                //    EndDate = endDate,
                //    ProgramClass = programClass,
                //    Teachers = teachers,
                //    Course = courses
                //};

                //LMSRepo.AddClassSchema(classSchema);

                return RedirectToAction("Details", "ProgramClass", new { id = programID});
            }
            catch
            {
                return View();
            }
        }

        // GET: Schema/Edit/5
        public ActionResult Edit(int id = 0)
        {
            ClassSchema classSchema = LMSRepo.GetClassSchemaByID(id);
            return View(classSchema);
        }

        // POST: Schema/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var classSchema = LMSRepo.GetClassSchemaByID(id);

            try
            {
                //DateTime startDate = Convert.ToDateTime(collection["StartDate"]);
                //DateTime endDate = Convert.ToDateTime(collection["EndDate"]);
                //var programClass = LMSRepo.GetProgramClassByID(classSchema.ProgramClass.ID);

                //classSchema.Title = "Schema for: " + programClass.ClassName + " From: " + startDate + "To" + endDate;
                //classSchema.StartDate = startDate;
                //classSchema.EndDate = endDate;

                //LMSRepo.EditClassSchema(classSchema);

                //return RedirectToAction("Details", "ProgramClass", new { id = programClass.ID });
                return null;
            }
            catch
            {
                return View(classSchema);
            }
        }
        
        public ActionResult Delete(int id = 0)
        {
            var classSchema  = LMSRepo.GetClassSchemaByID(id);
            LMSRepo.DeleteClassSchema(id);

            return RedirectToAction("Details");
        }
    }
}
