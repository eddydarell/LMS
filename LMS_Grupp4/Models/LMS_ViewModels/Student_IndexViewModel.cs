using LMS_Grupp4.Models.LMS_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS_Grupp4.Models.LMS_ViewModels
{
	public class Student_IndexViewModel
	{
		public List<Assignment> assignmentModel { get; set; }
        public List<Course> courseModel { get; set; }
        public List<CourseApplication> courseApplicationModel { get; set; }
        public List<IGrouping<string, LMSFile>> Files { get; set; }
		public ApplicationUser User { get; set; }

        public Student_IndexViewModel(List<Assignment> assignmentModel, List<Course> courseModel, List<CourseApplication> courseApplicationModel, List<IGrouping<string, LMSFile>> files)
        {
            this.assignmentModel = assignmentModel;
            this.courseModel = courseModel;
            this.courseApplicationModel = courseApplicationModel;
            this.Files = files;
        }

		public Student_IndexViewModel(List<Course> courses, ApplicationUser user)
		{
			this.courseModel = courses;
			this.User = user;
		}
	}
}