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
        public ActionResult SearchByEmail(string email, string resultFormat = "")
        {
            var userManager = LMSRepo.GetUserManager();
            var roleManager = LMSRepo.GetRoleManager();
            var studentRole = roleManager.Roles.FirstOrDefault(r => r.Name == "student");
            var students = userManager.Users.SingleOrDefault(u => u.Roles.FirstOrDefault(rr => rr.RoleId == studentRole.Id) != null && u.Email == email.ToLower());

            if(resultFormat == "json")
            {
                return Json(students);
            }
            else
            {
                return View(students);//To-Do: Create views for this controller
            }
        }
    }
}