using Awwsp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Awwsp.ViewModels
{
    public class TrophyCreateVM
    {
        public int TrophyID { get; set; }
        public string Name { get; set; }
        public IEnumerable<Photo> Photos { get; set; }
        public virtual ICollection<Child> Children { get; set; }
    }
}