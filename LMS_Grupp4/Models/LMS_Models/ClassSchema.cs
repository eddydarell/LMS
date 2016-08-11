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
        
        //Navigation Properties
        [ForeignKey("ProgramClass")]
        public int ProgramClassID { get; set; }
        public ProgramClass ProgramClass { get; set; }

        [ForeignKey("ApplicationUser")]
        public int TeacherID { get; set; }
        public virtual ICollection<ApplicationUser> Teachers { get; set; }

        [ForeignKey("Course")]
        public int CourseID { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}