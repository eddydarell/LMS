using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS_Grupp4.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index(string id = "")
        {
            ViewBag.UserID = id;
            return View();
        }

        public ActionResult ManageUsers()
        {
            return null;
        }
    }
}