using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Awwsp.Controllers
{
    [Authorize(Roles = "Admin,Parent,Coach,HedCoach")]
    public class ParentController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }


    }
}
