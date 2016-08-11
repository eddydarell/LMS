using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Grupp4.Models.LMS_Models
{
    public class Course
    {
        [Key]
        public int ID { get; set; }
        public string CourseName { get; set; }
        public string CourseTeacher { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<ClassSchema> ClassSchemes { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }
}