using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS_Grupp4.Models.LMS_Models
{
	public class ProgramClass
	{
		[Key]
		public int ID { get; set; }

		[MaxLength(20)]
        [Display(Name = "Class")]
        [Required]
        public string ClassName { get; set; }

        //Navigation properties
        public virtual ICollection<CourseApplication> ClassApplications { get; set; }
		public virtual ICollection<ApplicationUser> Users { get; set; }
		public virtual ICollection<ClassSchema> ClassSchemas { get; set; }
		public virtual ICollection<Course> Courses { get; set; }
	}
}