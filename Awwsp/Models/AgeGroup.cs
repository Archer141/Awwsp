using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Awwsp.Models
{
    public class AgeGroup
    {
        public int AgeGroupID { get; set; }
        public string Name { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
    }
}