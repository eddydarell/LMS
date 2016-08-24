using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS_Grupp4.Models.LMS_Models
{
    public class ClassSchema
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        public string Schedule { get; set; }

        [Required]
        [Display(Name = "Start")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End")]
        public DateTime EndDate { get; set; }
        
        //Navigation Properties
        public virtual Course Course { get; set; }
    }
}