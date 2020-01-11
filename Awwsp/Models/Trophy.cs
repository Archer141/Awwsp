using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Awwsp.Models
{
    public class Trophy
    {
        public int TrophyID { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        [Display(Name ="Choose photo or")]
        public Nullable<int> PhotoID { get; set; }
        public Photo Photo { get; set; }
        public Trophy()
        {
            this.Children = new HashSet<Child>();
        }
        public virtual ICollection<Child> Children { get; set; }
        public DateTime Date { get; set; }

    }
}