﻿using LMS_Grupp4.Models;
using LMS_Grupp4.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS_Grupp4.Controllers
{
	[Authorize(Roles = "student")]
    public class StudentController : Controller
    {
		static ApplicationDbContext context = new ApplicationDbContext();
		static RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context);
		RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);
		static UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(context);
		UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore);

		private LMSRepository LMSRepo = new LMSRepository();

        // GET: Student
        public ActionResult Index()
        {
		
            return View();
        }
    }
}