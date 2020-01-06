using Awwsp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Awwsp.ViewModels
{
    public class NotificationShowVM 
    {
        public IEnumerable<Notification> Notifications { get; set; }
    }
}