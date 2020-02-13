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
using System.Security.Claims;

namespace Awwsp.Controllers
{
    [Authorize]
    public class NewsController : BaseController
    {
        
        // GET: News
        [AllowAnonymous]
        public ActionResult Index()
        {

            return View(repository.GetNews().Reverse());
        }

        // GET: News/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // News news = await db.News.FindAsync(id);
            News news = repository.GetNewsByID(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: News/Create
        [Authorize(Roles ="Admin,HeadCoach,Coach")]
        public ActionResult Create()
        {
            ViewBag.PhotoID = new SelectList(repository.GetPhotos(), "PhotoID", "Name");
            return View();
        }

        // POST: News/Create
        [HttpPost]
        [Authorize(Roles ="Admin,HeadCoach,Coach")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NewsID,Text,Title,PhotoID")] News news)
        {
            if (ModelState.IsValid)
            {
                news.AuthorId = GetUserID();
                news.Date = DateTime.Now;
                repository.AddNews(news);
                return RedirectToAction("Index");
            }

            ViewBag.PhotoID = new SelectList(repository.GetPhotos(), "PhotoID", "Name", news.PhotoID);
            return View(news);
        }

        [Authorize(Roles ="Admin,HeadCoach,Coach")]
        // GET: News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = repository.GetNewsByID(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            ViewBag.PhotoID = new SelectList(repository.GetPhotos(), "PhotoID", "Name", news.PhotoID);
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin,HeadCoach,Coach")]
        public ActionResult Edit([Bind(Include = "NewsID,Text,Title,AuthorID,PhotoID")] News news)
        {
            if (ModelState.IsValid)
            {
                news.AuthorId = GetUserID();
                news.Date = DateTime.Now;
                repository.UpdateNews(news);
                return RedirectToAction("Index");
            }
            ViewBag.PhotoID = new SelectList(db.Photos, "PhotoID", "Name", news.PhotoID);
            return View(news);
        }

        // GET: News/Delete/5
        [Authorize(Roles ="Admin,HeadCoach,Coach")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = repository.GetNewsByID(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles ="Admin,HeadCoach,Coach")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repository.DeleteNews(id);
            return RedirectToAction("Index");
        }
        public string GetUserID()
        {
            string userIdValue = "";
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var userIdClaim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim != null)
                {
                    userIdValue = userIdClaim.Value;
                }
            }
            return userIdValue;
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
