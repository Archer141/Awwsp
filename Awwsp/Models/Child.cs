using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Awwsp.Models
{
    public class Child
    {
        public int ChildID { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "First name")]
        public string ChildFirstName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Last name")]
        public string ChildLastName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Date)]
        [Display(Name ="Date of birth")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Min 6 characters are required")]
        public string PasswordHash { get; set; }
        [Display(Name ="Is Accepted?")]
        public bool IsActive { get; set; }
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
        public Child()
        {
            this.Trophies = new HashSet<Trophy>();
        }
        public ICollection<Trophy> Trophies { get; set; }
        public int? AgeGroupID { get; set; }
        public AgeGroup AgeGroup { get; set; }
        public string FullName { get; set; }
        public bool IsSignOut { get; set; }

    }
}