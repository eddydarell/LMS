using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Grupp4.Models.LMS_Models
{
	public class File
	{
		[Key]
		public int FileID { get; set; }
		[MaxLength(45)]
		public string FileName { get; set; }
		[MaxLength(20)]
		public string FileFormat { get; set; }
		[MaxLength(25)]
		public string FileUploader { get; set; }
		public string URL { get; set; }
		public bool FilePublicVisibility { get; set; }
		public DateTime FileUploadDate { get; set; }
		public double FileSize { get; set; }

		public virtual ApplicationUser FileUser { get; set; }

		public virtual ICollection<Course> Courses { get; set; }
	}
}