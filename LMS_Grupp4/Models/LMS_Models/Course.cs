using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS_Grupp4.Models.LMS_Models
{
    public class Course
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Course")]
        [Required]
        public string CourseName { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<ClassSchema> ClassSchemes { get; set; }
        public virtual ICollection<ProgramClass> Classes { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }
}