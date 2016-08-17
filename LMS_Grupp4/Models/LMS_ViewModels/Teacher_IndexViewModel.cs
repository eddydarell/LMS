using LMS_Grupp4.Models.LMS_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS_Grupp4.Models.LMS_ViewModels
{
    public class Teacher_IndexViewModel
    {
        public IEnumerable<Assignment> Assignments { get; set; }
		public IEnumerable<Course> Courses { get; set; }

		public Teacher_IndexViewModel(IEnumerable<Assignment> assignments, IEnumerable<Course> courses)
		{
			this.Assignments = assignments;
			this.Courses = courses;
		}
    }
}