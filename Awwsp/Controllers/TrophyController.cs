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
    public class TrophyController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private AcademyRepository academyRepository;
        public TrophyController()
        {
            academyRepository = new AcademyRepository(db);
        }

        // GET: Trophies
        public ActionResult Index()
        {
            return View(academyRepository.GetTrophies());
        }

        // GET: Trophies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trophy trophy = academyRepository.GetTrophyById(id);
            if (trophy == null)
            {
                return HttpNotFound();
            }
            return View(trophy);
        }

        // GET: Trophies/Create
        public ActionResult Create()
        {
            ViewBag.PhotoID = new SelectList(academyRepository.GetPhotos().Where(x=>x.IsTrophy==true).ToList(), "PhotoID", "Name");
            return View();
        }

        // POST: Trophies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrophyID,Name,PhotoID")] Trophy trophy, HttpPostedFileBase image1)
        {
            if (ModelState.IsValid)
            {
                if (image1 != null)
                {
                    academyRepository.AddPhoto(new Photo { Date = DateTime.Now, Name = trophy.Name, IsTrophy = true }, image1);
                    trophy.PhotoID = academyRepository.GetPhotos().Last().PhotoID;
                }

                trophy.Date = DateTime.Now;
                academyRepository.AddTrophy(trophy);
                return RedirectToAction("Index");
            }
            ViewBag.PhotoID = new SelectList(academyRepository.GetPhotos().Where(x => x.IsTrophy == true).ToList(), "PhotoID", "Name");

            return View(trophy);
        }

        // GET: Trophies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trophy trophy = academyRepository.GetTrophyById(id);
            if (trophy == null)
            {
                return HttpNotFound();
            }
            ViewBag.PhotoID = new SelectList(academyRepository.GetPhotos().Where(x => x.IsTrophy == true).ToList(), "PhotoID", "Name");

            return View(trophy);
        }

        // POST: Trophies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrophyID,Name,PhotoID")] Trophy trophy, HttpPostedFileBase image1)
        {
            if (ModelState.IsValid)
            {
                if (image1 != null)
                {
                    academyRepository.AddPhoto(new Photo { Date = DateTime.Now, Name = trophy.Name, IsTrophy = true }, image1);
                    trophy.PhotoID = academyRepository.GetPhotos().Last().PhotoID;
                }
                academyRepository.UpdateTrophy(trophy);
                return RedirectToAction("Index");
            }
            ViewBag.PhotoID = new SelectList(academyRepository.GetPhotos().Where(x => x.IsTrophy == true).ToList(), "PhotoID", "Name");

            return View(trophy);
        }

        // GET: Trophies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trophy trophy = academyRepository.GetTrophyById(id);
            if (trophy == null)
            {
                return HttpNotFound();
            }
            return View(trophy);
        }

        // POST: Trophies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            academyRepository.DeleteTrophy(id);
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
