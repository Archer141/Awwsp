using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Awwsp.Models
{
    public class AgeGroup
    {
        public int AgeGroupID { get; set; }
        [Required(ErrorMessage ="Name is requuired")]
        [Display(Name="Age gropu name")]
        public string Name { get; set; }
        [Range(3, 17)]
        [LessThan("MaxAge",DependentPropertyDisplayName ="Max age",ErrorMessage ="Field max age must be grater than min age")]
        public int MinAge { get; set; }

        [Range(4, 18)]
        [LessThan("MinAge", DependentPropertyDisplayName="Min age", ErrorMessage ="Field min age must be lower than max age")]
        public int MaxAge { get; set; }
    }
}