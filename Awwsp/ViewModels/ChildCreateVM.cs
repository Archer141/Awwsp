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

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Imie")]
        public string ChildFirstName { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Nazwisko")]
        public string ChildLastName { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Podaje hasło")]
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Minimalna długość to 6")]
        public string PasswordHash { get; set; }
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
        
        public int AgeGroupID { get; set; }
        public AgeGroup AgeGroup { get; set; }

    }
}