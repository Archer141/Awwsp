using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Awwsp.ViewModels
{
    public class EventVM
    {
        [Display(Name ="Id")]
        public int Id { get; set; }
        [Display(Name ="Title")]
        public string Title { get; set; }
        [Display(Name = "Start")]
        public string Start { get; set; }
        [Display(Name ="End")]
        public string End { get; set; }
        [Display(Name ="Color")]
        public string Color { get; set; }  
        [Display(Name ="Text color")]
        public string TextColor { get; set; }
        [Display(Name ="All day")]
        public bool AllDay { get; set; }
    }
}