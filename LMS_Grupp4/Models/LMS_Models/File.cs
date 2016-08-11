using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS_Grupp4.Models.LMS_Models
{
	public class File
	{
		[Key]
		public int ID { get; set; }

		[MaxLength(45)]
        [Display(Name = "File Name")]
        [Required]
        public string Name { get; set; }

		[MaxLength(20)]
        [Display(Name = "Format")]
        public string Format { get; set; }

        [Display(Name = "URL")]
        [Required]
        public string URL { get; set; }

        [Display(Name = "Visible For")]
        [Required]
        public bool IsPublicVisible { get; set; }

        [Display(Name = "Uploaded On")]
        [Required]
        public DateTime UploadDate { get; set; }

        [Range(0, 9999999999.99)]
        [Display(Name = "Size")]
        public double Size { get; set; }
		
        //Navigation Properties
		public virtual ApplicationUser Uploader { get; set; }
		public virtual ICollection<Course> Courses { get; set; }
	}
}