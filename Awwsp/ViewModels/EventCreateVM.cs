using Awwsp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Awwsp.ViewModels
{
    public class EventCreateVM
    {

        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Start date")]
        [DataType(DataType.Date)]
        public DateTime Start { get; set; }

        [Display(Name = "End date")]
        [DataType(DataType.Date)]
        public DateTime End { get; set; }

        [Display(Name = "All day?")]
        public bool AllDay { get; set; }
        [Display(Name = "Repeated throught")]
        [Range(1, 52, ErrorMessage = "Max value is 52, min 1")]
        public int? RepetedThroughtWeeks { get; set; }
        public int AgeGroupID { get; set; }
        public AgeGroup AgeGroup { get; set; }
    }
}