using LMS_Grupp4.Models.LMS_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS_Grupp4.Models.LMS_ViewModels
{
    public class Teacher_IndexViewModel
    {
        public List<Assignment> assignmentModel { get; set; }
        public List<Course> courseModel { get; set; }
        public List<CourseApplication> courseApplicationModel { get; set; }
        public List<IGrouping<string, LMSFile>> SubFiles { get; set; }

        public Teacher_IndexViewModel(List<Assignment> assignmentModel, List<Course> courseModel, List<CourseApplication> courseApplicationModel, List<IGrouping<string, LMSFile>> subFiles)
        {
            // TODO: Complete member initialization
            this.assignmentModel = assignmentModel;
            this.courseModel = courseModel;
            this.courseApplicationModel = courseApplicationModel;
            this.SubFiles = subFiles;
        }

        public Teacher_IndexViewModel(List<Assignment> assignmentModel, List<Course> courseModel, List<CourseApplication> courseApplicationModel)
        {
            // TODO: Complete member initialization
            this.assignmentModel = assignmentModel;
            this.courseModel = courseModel;
            this.courseApplicationModel = courseApplicationModel;
        }

        //public Teacher_IndexViewModel(IEnumerable<Assignment> assignments, IEnumerable<Course> courses, IEnumerable<CourseApplication> courseApplications, IEnumerable<ApplicationUser> students)
        ////        public Teacher_IndexViewModel(IEnumerable<Course> courses)
        //{
        //    this.CourseApplications = courseApplications;
        //    this.Assignments = assignments;
        //    this.Courses = courses;
        //    this.Students = students;
        //    //            this.ApplicationUser = studentList;
        //}
    }
}