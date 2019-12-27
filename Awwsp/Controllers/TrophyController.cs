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
        private AcademyRepository repository;
        public TrophyController()
        {
            repository = new AcademyRepository(db);
        }

        // GET: Trophies
        public async Task<ActionResult> Index()
        {
            return View(await db.Trophies.ToListAsync());
        }
        
        // GET: Trophies/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trophy trophy = await db.Trophies.FindAsync(id);
            if (trophy == null)
            {
                return HttpNotFound();
            }
            return View(trophy);
        }

        // GET: Trophies/Create
        public ActionResult Create()
        {
            ViewBag.PhotoID = new SelectList(repository.GetPhotos(), "PhotoID", "Name");
            return View();
        }

        // POST: Trophies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TrophyID,Name")] Trophy trophy)
        {
            if (ModelState.IsValid)
            {
                trophy.Date = DateTime.Now;

                db.Trophies.Add(trophy);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(trophy);
        }

        // GET: Trophies/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trophy trophy = await db.Trophies.FindAsync(id);
            if (trophy == null)
            {
                return HttpNotFound();
            }
            return View(trophy);
        }

        // POST: Trophies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TrophyID,Name")] Trophy trophy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trophy).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(trophy);
        }

        // GET: Trophies/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trophy trophy = await db.Trophies.FindAsync(id);
            if (trophy == null)
            {
                return HttpNotFound();
            }
            return View(trophy);
        }

        // POST: Trophies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Trophy trophy = await db.Trophies.FindAsync(id);
            db.Trophies.Remove(trophy);
            await db.SaveChangesAsync();
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
