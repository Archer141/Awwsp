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
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private AcademyRepository academyRepository;
        public NewsController()
        {
            academyRepository = new AcademyRepository(db);
        }
        // GET: News
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            var news = db.News.Include(n => n.Photo);
            return View(await news.ToListAsync());
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
            News news = academyRepository.GetNewsByID(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: News/Create
        public ActionResult Create()
        {
            ViewBag.PhotoID = new SelectList(academyRepository.GetPhotos(), "PhotoID", "Name");
            return View();
        }

        // POST: News/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NewsID,Text,Title,PhotoID")] News news)
        {
            if (ModelState.IsValid)
            {
                news.AuthorId = GetUserID();
                academyRepository.AddNews(news);
                return RedirectToAction("Index");
            }

            ViewBag.PhotoID = new SelectList(academyRepository.GetPhotos(), "PhotoID", "Name", news.PhotoID);
            return View(news);
        }

        // GET: News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = academyRepository.GetNewsByID(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            ViewBag.PhotoID = new SelectList(academyRepository.GetPhotos(), "PhotoID", "Name", news.PhotoID);
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NewsID,Text,Title,AuthorID,PhotoID")] News news)
        {
            if (ModelState.IsValid)
            {
                academyRepository.UpdateNews(news);
                return RedirectToAction("Index");
            }
            ViewBag.PhotoID = new SelectList(db.Photos, "PhotoID", "Name", news.PhotoID);
            return View(news);
        }

        // GET: News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = academyRepository.GetNewsByID(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = academyRepository.GetNewsByID(id);
            academyRepository.DeleteNews(id);
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
