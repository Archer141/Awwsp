using Awwsp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Awwsp.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<News> NewsTop3 { get; set; }
        public IEnumerable<Notification> Notifications { get; set; }
    }
}