using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMS_Grupp4.Models.LMS_Models
{
	public class Evaluation
	{
		[Key]
		public int ID { get; set; }

		[MaxLength(2)]
		[Display(Name = "Mark")]
		public string Mark { get; set; }

		[MaxLength(144)]
		[Display(Name = "Comment")]
		public string Message { get; set; }

		[Display(Name = "Passed")]
		public bool IsPassed { get; set; }

		[Display(Name = "Score")]
		public double? Score { get; set; }

		[Display(Name = "Percentage")]
		public double? Percentage { get; set; }


		public virtual ApplicationUser Student { get; set; }
		public virtual LMSFile LMSFile { get; set; }
		public virtual Assignment Assignment { get; set; }
	}
}