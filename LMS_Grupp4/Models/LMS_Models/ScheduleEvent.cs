using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMS_Grupp4.Models.LMS_Models
{
    public class ScheduleEvent
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Start Time")]
        public string StartTime { get; set; }

        [Display(Name = "End Time")]
        public string EndTime { get; set; }

        [Display(Name = "All Day")]
        public bool IsAllDay { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Priority")]
        [Range(1, 3, ErrorMessage = "Priority out of bounds")]
        public int Priority { get; set; }//Event priority(Exam have higher priority than tests)

        [Display(Name = "Status")]
        public bool IsExpired { get; set; }//Is expired

        [Display(Name = "Lock Status")]
        public bool IsLocked { get; set; }//Is locked for later changes

        [Display(Name = "Periodicity")]
        [Range(0,4, ErrorMessage = "Periodicity out of bounds")]
        public int Periodicity { get; set; }//Repeat periodicity

        [Display(Name = "Nature")]
        [Range(1, 6, ErrorMessage ="Nature out of bounds")]
        public int EventNature { get; set; }//Test, exam, lecture, etc

        //Navigation properties
        [ForeignKey("Course")]
        public virtual int CourseID { get; set; }
        public virtual Course Course { get; set; }
    }
}