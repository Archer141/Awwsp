using Awwsp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Awwsp.ViewModels
{
    public class ChildCreateVM
    {
        public int ChildID { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First name")]
        public string ChildFirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last name")]
        public string ChildLastName { get; set; }

        [Required(ErrorMessage = "Child date of birth is required to assign to proper age group")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be min 6 characters long")]
        public string PasswordHash { get; set; }
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
        
        public int AgeGroupID { get; set; }
        public AgeGroup AgeGroup { get; set; }

    }
}