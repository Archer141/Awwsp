using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Awwsp.Models
{
    public class Event
    {

        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Start")]
        public DateTime Start { get; set; }

        [Display(Name = "End")]
        public DateTime End { get; set; }

        [Display(Name = "Color")]
        public string Color { get; set; }

        [Display(Name = "Text color")]
        public string TextColor { get; set; }

        [Display(Name = "All day")]
        public bool AllDay { get; set; }

        public int AgeGroupID { get; set; }
        public AgeGroup AgeGroup { get; set; }
    }
}