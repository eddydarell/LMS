using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS_Grupp4.Controllers
{
    public class ErrorController : Controller
    {
     
        // GET: Custom Errors      
        public ActionResult ErrorHandler400()
        {
            return View();
        }

        public ActionResult ErrorHandler401()
        {
            return View();
        }

        public ActionResult ErrorHandler403()
        {
            return View();
        }

        public ActionResult ErrorHandler404()
        {
            return View();
        }

        public ActionResult ErrorHandler500()
        {
            return View();
        }
    }

}