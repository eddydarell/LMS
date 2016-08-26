using LMS_Grupp4.Models.LMS_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS_Grupp4.Models.LMS_ViewModels
{
    //This viewModel contains random information about the application
    //This is meant to be shown to the user before login
    public class Home_IndexViewModel
    {
        public int TeachersCount { get; set; }
        public int StudentsCount { get; set; }
        public int CourseCount { get; set; }
        public Course MostPopularCourse { get; set; }
    }
}