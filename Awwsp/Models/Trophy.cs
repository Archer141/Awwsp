using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Awwsp.Models
{
    public class Trophy
    {
        public int TrophyID { get; set; }
        public string Name { get; set; }
        public Trophy()
        {
            this.Children = new HashSet<Child>();
        }
        public virtual ICollection<Child> Children { get; set; }
    }
}