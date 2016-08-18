using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS_Grupp4.Models.LMS_Models
{
	public class LMSFile
	{
		[Key]
		public int ID { get; set; }

		//[MaxLength(45)] Commented this out as it generated validation problems
        //To-Do: Fix this before deploy
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

        [Range(0, 99999999999.99)]
        [Display(Name = "Size")]
        public double Size { get; set; }
		
        //Navigation Properties
		public virtual ApplicationUser Uploader { get; set; }
		public virtual ICollection<Course> Courses { get; set; }
        public virtual Assignment Assignment { get; set; }
	}
}