using Awwsp.Data;
using Awwsp.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Awwsp.Controllers
{
    [Authorize]
    public class PlayerController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        private static AcademyRepository repository;

        public PlayerController()
        {
            repository = new AcademyRepository(db);
        }

        // GET: Player
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Training()
        {
            return View();
        }
        public ActionResult YourTrophies()
        {
            List<Trophy> list;
            if (User.IsInRole("Admin") || User.IsInRole("Coach") || User.IsInRole("Parent") || User.IsInRole("HeadCoach"))
            {
                list = repository.GetTrophies().Where(x => x.Children.Where(a => a.UserID == User.Identity.GetUserId()).Select(b => b.UserID).FirstOrDefault() == User.Identity.GetUserId()).ToList();
            }
            else
            {
                var username = User.Identity.GetUserName();
          
                list = repository.GetTrophies().Where(x => x.Children.Where(a => a.FullName == username).Select(b => b.FullName).FirstOrDefault() == username).ToList();
            }
            return View(list);
        }
    }
}