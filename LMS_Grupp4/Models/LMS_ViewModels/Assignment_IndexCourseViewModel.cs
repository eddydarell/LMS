using LMS_Grupp4.Models.LMS_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS_Grupp4.Models.LMS_ViewModels
{
	public class Assignment_IndexCourseViewModel
	{
		public string CourseName;
		public IEnumerable<Assignment> Assignments;

		public Assignment_IndexCourseViewModel(string courseName, IEnumerable<Assignment> assignments)
		{
			this.CourseName = courseName;
			this.Assignments = assignments;
		}
	}
}