﻿using LMS_Grupp4.Models.LMS_Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS_Grupp4.Models.LMS_ViewModels
{
	public class Assignment_CreateViewModel
	{
		public Assignment Assignment { get; set; }
		public IEnumerable<ApplicationUser> Students { get; set; }


		[Display(Name = "Students")]
		public int SelectStudentID { get; set; }
		public IEnumerable<SelectListItem> SelectedStudent
		{ 
			get {return new SelectList(Students, "Id", "Email");}
		}

		public Assignment_CreateViewModel(IEnumerable<ApplicationUser> students)
		{
			this.Assignment = new Assignment();
			this.Students = students;
		}
	}

	//Extension class
	public static class AssignmentCreateStudentDropdownListExtensionClass
	{
		//Extension method to help build a dropdown list with students
		public static IEnumerable<SelectListItem> ToSelectListItems(
			  this IEnumerable<ApplicationUser> students, string selectedId)
		{
			return
				students.OrderBy(stu => stu.RealName)
					  .Select(stu =>
						  new SelectListItem
						  {
							  Selected = (stu.Id == selectedId),
							  Text = stu.RealName + "(" + stu.Email + ")",
							  Value = stu.Id.ToString()
						  });
		}
	}
}