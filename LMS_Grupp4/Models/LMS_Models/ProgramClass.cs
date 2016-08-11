using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMS_Grupp4.Models.LMS_Models
{
	public class ProgramClass
	{
		[Key]
		public int ID { get; set; }
		[MaxLength(20)]
		public string ClassName { get; set; }

		public ICollection<ApplicationUser> ApplicationUsers { get; set; }
		public ICollection<ClassSchema> ClassSchemas { get; set; }
		public ICollection<Course> Courses { get; set; }
	}
}