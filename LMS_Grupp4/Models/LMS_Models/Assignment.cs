using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LMS_Grupp4.Models.LMS_Models
{
	[Bind(Exclude = "ID")]
	public class Assignment
	{
		[Key]
		public int ID { get; set; }

		[MaxLength(100)]
		[Display(Name = "Assignment")]
		[Required]
		public string Name { get; set; }

		[Display(Name = "Due Date")]
		[Required]
		public DateTime? DueDate { get; set; }

		[Display(Name = "Issue Date")]
		[Required]
		public DateTime IssueDate { get; set; }

		[MaxLength(2)]
		[Display(Name = "Mark")]
		public string Mark { get; set; }

		[Display(Name = "Passed")]
		public bool IsPassed { get; set; }

		[Display(Name = "Expired")]
		public bool IsExpired { get; set; }

		[Display(Name = "Score")]
		public int? Score { get; set; }

		[Display(Name = "Percentage")]
		public double? Percentage { get; set; }

		[Display(Name = "Max Score")]
		[Required]
		public int MaxScore { get; set; }

        public virtual Course Course { get; set; }
        public virtual ICollection<ApplicationUser> Students { get; set; }
    }
}