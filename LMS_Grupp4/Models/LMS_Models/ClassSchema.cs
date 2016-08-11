using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public ProgramClass ProgramClass { get; set; }
        public virtual ICollection<ApplicationUser> Teachers { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}