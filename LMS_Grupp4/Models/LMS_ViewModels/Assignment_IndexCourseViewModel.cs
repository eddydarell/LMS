using LMS_Grupp4.Models.LMS_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS_Grupp4.Models.LMS_ViewModels
{
	public class Assignment_IndexCourseViewModel
	{
		public Course Course;
		public IEnumerable<Assignment> Assignments;

		public Assignment_IndexCourseViewModel(Course course, IEnumerable<Assignment> assignments)
		{
			this.Course = course;
			this.Assignments = assignments;
		}
	}
}