using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace LMS_Grupp4.Models.LMS_ViewModels
{
    //This a viewodel class created to contains a little more info
    //THan the base model classes
    public class Admin_ManageUserViewModel
    {
        [Key]
        public int ID { get; set; }

        public string UserID { get; set; }

        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "Email")]
        public string UserEmail { get; set; }

        [Display(Name = "Real Name")]
        public string UserRealName { get; set; }

        [Display(Name = "Roles")]
        public IEnumerable<string> UserRoles { get; set; }

        [Display(Name = "Phone Number")]
        public string UserPhoneNumber { get; set; }
    }
}