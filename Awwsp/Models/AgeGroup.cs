﻿using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Awwsp.Models
{
    public class AgeGroup
    {
        public int AgeGroupId { get; set; }
        [Required(ErrorMessage ="Name is requuired")]
        [Display(Name="Team")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Max age is required")]
        [Range(3, 17,ErrorMessage ="Min age must be grater than 3 and lower than 17")]
        [LessThan("MaxAge",DependentPropertyDisplayName ="Max age",ErrorMessage ="Field max age must be grater than min age")]
        public int MinAge { get; set; }
        [Required(ErrorMessage = "Min age is required")]
        [Range(4, 18, ErrorMessage = "Max age must be grater than 4 and lower than 18")]
        [GreaterThan("MinAge", DependentPropertyDisplayName="Min age", ErrorMessage ="Field min age must be lower than max age")]
        public int MaxAge { get; set; }
        public ICollection<Child> Children { get; set; }
        public ICollection<Event> Events { get; set; }

    }
}