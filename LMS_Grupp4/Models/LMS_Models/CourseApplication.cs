using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMS_Grupp4.Models.LMS_Models
{
    //This class contains the application object
    //An application is sent by a student to a teacher or teachers responsible of that course
    public class CourseApplication
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Message")]
        [MaxLength(500)]
        [Required]
        public string Message { get; set; }

        [Display(Name = "Status")]
        public bool Status { get; set; }//False = Pending application, true = Answered application

        [Display(Name = "Acceptance Status")]
        public bool IsAccepted { get; set; } //True = Accepted, false = rejected

        [Display(Name = "Emission Date")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Evaluation Date")]
        public DateTime? EvaluationDate { get; set; }

        //Navigation properties
        public virtual ProgramClass ProgramClass { get; set; }
        public virtual Course Course { get; set; }
        public virtual ApplicationUser Student { get; set; }
        public virtual ICollection<ApplicationUser> Teachers { get; set; }
    }
}