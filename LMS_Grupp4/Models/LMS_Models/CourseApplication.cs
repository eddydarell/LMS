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
        [MaxLength(250)]
        [Required]
        public string Message { get; set; }

        //Navigation properties
        public ProgramClass ProgramClass { get; set; }
        public Course Course { get; set; }
        public ApplicationUser Student { get; set; }
        public virtual ICollection<ApplicationUser> Teachers { get; set; }
    }
}