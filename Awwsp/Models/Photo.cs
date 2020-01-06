using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Awwsp.Models
{
    public class Photo
    {
        public int PhotoID { get; set; }
        [Required(ErrorMessage ="Name of photo is reqiered")]
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public virtual ICollection<News> News { get; set; }
        public DateTime Date { get; set; }
        [Display(Name="Is this a photo of the cup or trophy?")]
        public bool IsTrophy { get; set; }

    }
}