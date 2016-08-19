using LMS_Grupp4.Models.LMS_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS_Grupp4.Models.LMS_ViewModels
{
    //View model to build application form
    public class Course_ApplicationViewModel
    {
        //Private variables
        //To-Do: use it for PRogramclass instead
        //public List<ProgramClass> _ProgramClasses { get; set; }

        [Display(Name = "Number" )]
        public int ID { get; set; }

        [Display(Name = "Message")]
        [MaxLength(500)]
        public string Message { get; set; }

        [Display(Name = "Student Name")]
        public string StudentRealName { get; set; }

        public int CourseID { get; set; }

        [Display(Name = "Course")]
        public string CourseName { get; set; }

        [Display(Name = "Class")]
        public int SelectedProgramClassID { get; set; }

        //To-Do: Use this for program instead
        //public IEnumerable<SelectListItem> PClass
        //{
        //    get { return new SelectList(_ProgramClasses, "Id", "ClassName"); }
        //}
    }

    //Extension class
    public static class ExtensionClass
    {
        //Extension method to help build a dropdown list for this program class
        public static IEnumerable<SelectListItem> ToSelectListItems(
              this IEnumerable<ProgramClass> programClass, int selectedId)
        {
            return
                programClass.OrderBy(pc => pc.ClassName)
                      .Select(pc =>
                          new SelectListItem
                          {
                              Selected = (pc.ID == selectedId),
                              Text = pc.ClassName,
                              Value = pc.ID.ToString()
                          });
        }
    }
}