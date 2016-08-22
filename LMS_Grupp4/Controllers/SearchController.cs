using LMS_Grupp4.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS_Grupp4.Controllers
{
    public class SearchController : Controller
    {
        LMSRepository LMSRepo = new LMSRepository();

        
        
        [Authorize]
        [HttpGet]
        public ActionResult SearchUserByEmail(string email, string resultFormat)
        {
            var userManager = LMSRepo.GetUserManager();
            var roleManager = LMSRepo.GetRoleManager();
            var studentRole = roleManager.Roles.FirstOrDefault(r => r.Name == "student");
            var student = userManager.Users.SingleOrDefault(u => u.Roles.FirstOrDefault(rr => rr.RoleId == studentRole.Id) != null && u.Email == email.ToLower());

            try
            {
                if (resultFormat == "json")
                {
                    return Json(new { ID = student.Id, Name = student.RealName, Email = student.Email }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return View(student);//To-Do: Create views for this controller
                }
            }
            catch(Exception e)
            {
                if(e is NullReferenceException)
                {
                    return Json(new { error = "Error, student not found" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { error = "Error: " + e.ToString() }, JsonRequestBehavior.AllowGet);
                }
            }
            
        }

        [Authorize]
        public ActionResult SearchCourseByName(string name, string resultFormat = "")
        {
            var courses = LMSRepo.GetAllCourses().Where(c => c.CourseName.ToLower().Contains(name.ToLower())).OrderBy(c => c.CourseName).ToList();

            if (resultFormat == "json")
            {
                return Json(courses);
            }
            else
            {
                return View(courses);//To-Do: Create views for this controller
            }
        }

        [Authorize]
        public ActionResult SearchProgramClassByName(string name, string resultFormat = "")
        {
            var programClasses = LMSRepo.GetAllProgramClasses().Where(c => c.ClassName.ToLower().Contains(name.ToLower())).OrderBy(c => c.ClassName).ToList();

            if (resultFormat == "json")
            {
                return Json(programClasses);
            }
            else
            {
                return View(programClasses);//To-Do: Create views for this controller
            }
        }

        [Authorize]
        public ActionResult SearchFileByName(string name, string resultFormat = "")
        {
            var files = LMSRepo.GetAllFiles().Where(c => c.Name.ToLower().Contains(name.ToLower())).OrderBy(c => c.Name).ToList();

            if (resultFormat == "json")
            {
                return Json(files);
            }
            else
            {
                return View(files);//To-Do: Create views for this controller
            }
        }

        //To-Do: Add groupby and orderBy 
    }
}