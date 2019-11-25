using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Awwsp.Models
{
    public class Photo
    {
        public int PhotoID { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public virtual ICollection<News> News { get; set; }
    }
}