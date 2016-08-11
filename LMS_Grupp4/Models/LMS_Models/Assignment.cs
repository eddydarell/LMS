using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Grupp4.Models.LMS_Models
{
    public class Assignment
    {
        [Key]
        public int ID { get; set; }
        public string AssignmentName { get; set; }
        public DateTime AssignmentDueDate { get; set; }
        public DateTime AssignmentReleaseDate { get; set; }
        public string AssignmentCourse { get; set; }
        public string AssignmentMark { get; set; }
        public bool AssignmentPassed { get; set; }
        public double AssignmentPoints { get; set; }
        public double AssignmentPercents { get; set; }
        public double AssignmentMaxPoint { get; set; }

        [ForeignKey("Course")] 
        public int CourseID { get; set; }
        [ForeignKey("Assignment")]
        public int AssignmentID { get; set; }
        public virtual Course CourseUser { get; set; }
        public virtual ApplicationUser AssignmentUser { get; set; }

    }

}