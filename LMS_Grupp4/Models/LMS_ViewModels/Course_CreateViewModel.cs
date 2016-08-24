using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMS_Grupp4.Models.LMS_ViewModels
{
    public class Course_CreateViewModel
    {
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }

        [Display(Name = "Description")]
        public string CourseDescription { get; set; }

        [Display(Name = "Start Date")]
        public DateTime ScheduleStartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime ScheduleEndDate { get; set; }

        [Display(Name = "Schedule")]
        public string Schedule { get; set; }
    }
}