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
		public int ProgramClassID { get; set; }
		[MaxLength(20)]
		public string ClassName { get; set; }
		public string ClassTeachers { get; set; }

		public ICollection<ClassSchema> ClassSchemas { get; set; }

		public ICollection<ApplicationUser> ApplicationUsers { get; set; }

	}
}