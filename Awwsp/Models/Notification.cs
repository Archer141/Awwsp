using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Awwsp.Models
{
    public class Notification
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Title")]
        public string Title { get; set; }
        [Required]
        [Display(Name ="Description")]
        public string Text { get; set; }

        [Required]
        [Display(Name = "Team for")]
        public int AgeGroupId { get; set; }
      
        [Display(Name = "Team for")]
        public AgeGroup AgeGroup { get; set; }
        public bool Perceived { get; set; }

    }
}