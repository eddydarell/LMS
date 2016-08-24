using LMS_Grupp4.Models.LMS_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS_Grupp4.Models.LMS_ViewModels
{
    public class Teacher_IndexViewModel
    {
        public IEnumerable<CourseApplication> CourseApplications { get; set; }
        public IEnumerable<Assignment> Assignments { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<ApplicationUser> Students{ get; set; }

//        public Teacher_IndexViewModel(IEnumerable<Assignment> assignments, IEnumerable<Course> courses, IEnumerable<ApplicationUser> studentList)
        public Teacher_IndexViewModel(IEnumerable<Assignment> assignments, IEnumerable<Course> courses, IEnumerable<CourseApplication> courseApplications, IEnumerable<ApplicationUser> students)
//        public Teacher_IndexViewModel(IEnumerable<Course> courses)
        {
            this.CourseApplications = courseApplications;
            this.Assignments = assignments;
            this.Courses = courses;
            this.Students = students;
//            this.ApplicationUser = studentList;
        }

        public Teacher_IndexViewModel(IEnumerable<Course> courses, IEnumerable<ApplicationUser> students)
        //        public Teacher_IndexViewModel(IEnumerable<Course> courses)
        {
            this.Courses = courses;
            this.Students = students;
            //            this.ApplicationUser = studentList;
        }
    }
}