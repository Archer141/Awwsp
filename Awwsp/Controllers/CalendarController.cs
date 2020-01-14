using Awwsp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Awwsp.Controllers
{
    public class CalendarController : Controller
    {
        // GET: Callendar
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetEvents()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var events = context.Events.ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
    }
}