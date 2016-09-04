using LMS_Grupp4.Models.LMS_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS_Grupp4.Models.LMS_ViewModels
{
	public class Assignment_IndexUserViewModel
	{
		public IEnumerable<IGrouping<string, Assignment>> TeacherAssignments;
		public IEnumerable<IGrouping<string, Assignment>> StudentAssignments;
		
		public Assignment_IndexUserViewModel(IEnumerable<IGrouping<string, Assignment>> teacherAssignments, IEnumerable<IGrouping<string, Assignment>> studentAssignments)
		{
			this.TeacherAssignments = teacherAssignments;
			this.StudentAssignments = studentAssignments;
		}
	}
}