﻿using LMS_Grupp4.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS_Grupp4.Controllers
{
    public class StudentController : Controller
    {
		private LMSRepository LMSRepo = new LMSRepository();

        // GET: Student
        public ActionResult Index()
        {
            return View(LMSRepo);
        }
    }
}