using Awwsp.Data;
using Awwsp.Models;
using Awwsp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Awwsp.Controllers
{
    public class DefaultController : Controller
    {
        public NotificationShowVM NotificationShowVM { get; set; }
        public AcademyRepository AcademyRepository { get; set; }
        private ApplicationDbContext dbContext;
        public DefaultController()
        {
            dbContext = new ApplicationDbContext();
            this.AcademyRepository = new AcademyRepository(dbContext);
            this.NotificationShowVM = new NotificationShowVM();
            this.NotificationShowVM.Notifications = AcademyRepository.GetNotifications();
            this.ViewBag.NotificationShowVM = this.NotificationShowVM;
            this.ViewBag.dupa = "Dupa";
        }
    }
}