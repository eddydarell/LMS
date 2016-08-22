using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using LMS_Grupp4.Repositories;

namespace LMS_Grupp4.Controllers
{
	[Authorize(Roles = "student, teacher")]
    public class AssignmentController : Controller
    {
        LMSRepository LMSRepo = new LMSRepository();


        public ActionResult Index(string id = "")
        {
			if (String.IsNullOrWhiteSpace(id))
			{
				id = User.Identity.GetUserId();
			}
			ViewBag.UserID = id;

			var user = LMSRepo.GetUserManager().FindById(id);
			var assignmentModelList = user.Assignments.ToList();

            return View(assignmentModelList);
        }

		public ActionResult Details(int? id)
		{
			var assignment = LMSRepo.GetAssignmentByID(id);

			return View(assignment);
		}
    }
}