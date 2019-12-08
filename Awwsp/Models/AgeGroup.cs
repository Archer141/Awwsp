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
        [Required]
        public string Name { get; set; }
        [Range(3, 17)]
        [LessThan("MaxAge")]
        public int MinAge { get; set; }

        [Range(4, 18)]
        [GreaterThan("MinAge")]
        public int MaxAge { get; set; }
    }
}