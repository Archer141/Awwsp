using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Awwsp.Models;
using Awwsp.Data;

namespace Awwsp.Controllers
{
    [Authorize]
    public class NotificationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private AcademyRepository academyRepository;

        public NotificationController()
        {
            academyRepository = new AcademyRepository(db);
        }
        // GET: notifications
        public ActionResult Index()
        {
            var notifications = academyRepository.GetNotifications();
            return View(notifications);
        }

        // GET: notifications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = academyRepository.GetNotificationById(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            return View(notification);
        }
        [Authorize(Roles = "Admin,HeadCoach,Coach")]
        // GET: notifications/Create
        public ActionResult Create(int? id)
        {
            if (id!=null)
            {
                ViewBag.AgeGroupId = new SelectList(db.AgeGroups, "AgeGroupID", "Name");
                return View(new Notification { ChildId = id });
            }
            else
            {
                ViewBag.AgeGroupId = new SelectList(db.AgeGroups, "AgeGroupID", "Name");
                return View();
            }
           
        }

        // POST: notifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin,HeadCoach,Coach")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Text,AgeGroupId,ChildId")] Notification notification)
        {
            if (ModelState.IsValid)
            {
                academyRepository.AddNotification(notification);
                return RedirectToAction("Index");
            }

            ViewBag.AgeGroupId = new SelectList(db.AgeGroups, "AgeGroupID", "Name", notification.AgeGroupId);
            return View(notification);
        }

        [Authorize(Roles = "Admin,HeadCoach,Coach")]// GET: notifications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = academyRepository.GetNotificationById(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgeGroupId = new SelectList(db.AgeGroups, "AgeGroupID", "Name", notification.AgeGroupId);
            return View(notification);
        }

        // POST: notifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin,HeadCoach,Coach")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Text,AgeGroupId")] Notification notification)
        {
            if (ModelState.IsValid)
            {
                academyRepository.UpdateNotification(notification);
                return RedirectToAction("Index");
            }
            ViewBag.AgeGroupId = new SelectList(db.AgeGroups, "AgeGroupID", "Name", notification.AgeGroupId);
            return View(notification);
        }

        // GET: notifications/Delete/5
        [Authorize(Roles = "Admin,HeadCoach,Coach")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = academyRepository.GetNotificationById(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            return View(notification);
        }

        // POST: notifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin,HeadCoach,Coach")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            academyRepository.DeleteNotification(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
