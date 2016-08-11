using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMS_Grupp4.Models.LMS_Models
{
    public class ClassSchema
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Start")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End")]
        public DateTime EndDate { get; set; }

        //Uses a specific schedule class that contains the information to build a schedule
        public Schedule Schedule { get; set; }
        
        //Navigation Properties
        [ForeignKey("ProgramClass")]
        public int ProgramClassID { get; set; }
        public ProgramClass ProgramClass { get; set; }

        [ForeignKey("Course")]
        public int CourseID { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }

    public class Schedule
{
    public string Time { get; set; }
    public string Course { get; set; }
    public string Room { get; set; }
    public string Teacher { get; set; }
}
}