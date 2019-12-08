using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Awwsp.Models
{
    public class News
    {
        public int NewsID { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public string AuthorId { get; set; }
        public Nullable<int> PhotoID { get; set; }
        public Photo Photo { get; set; }
    }
}