using LMS_Grupp4.Models.LMS_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS_Grupp4.Models.LMS_ViewModels
{
	public class Assignment_IndexUserViewModel
	{
		public IEnumerable<Assignment> TeacherAssignments;
		public IEnumerable<Assignment> StudentAssignments;
		
		public Assignment_IndexUserViewModel(IEnumerable<Assignment> teacherAssignments, IEnumerable<Assignment> studentAssignments)
		{
			this.TeacherAssignments = teacherAssignments;
			this.StudentAssignments = studentAssignments;
		}
	}
}