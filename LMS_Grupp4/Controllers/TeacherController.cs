using LMS_Grupp4.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS_Grupp4.Controllers
{
    public class TeacherController : Controller
    {
        private LMSRepository _repository = new LMSRepository();

       //Get Teacher
        public ActionResult Index()
        {
            return View();
        }

    }
}