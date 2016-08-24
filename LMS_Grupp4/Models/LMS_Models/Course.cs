using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace LMS_Grupp4.Models.LMS_Models
{

    [Bind(Exclude = "CreationDate, ID")]//Added Bind Exclude
    public class Course
    {
        [Key, ForeignKey("ClassSchema")]
        public int ID { get; set; }

        [Display(Name = "Course")]
        [Required]
        public string CourseName { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Created On")] //Added creation date to course
        public DateTime CreationDate { get; set; }

        //Navigation properties
        public virtual ClassSchema ClassSchema { get; set; }
        public virtual ICollection<CourseApplication> CourseApplications { get; set; }
        public virtual ICollection<ApplicationUser> Teachers { get; set; }
        public virtual ICollection<ApplicationUser> Students { get; set; }
        public virtual ICollection<ProgramClass> Classes { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<LMSFile> Files { get; set; }
    }
}