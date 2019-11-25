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
using Awwsp.ViewModels;
using System.Security.Claims;

namespace Awwsp.Controllers
{
    [Authorize]
    public class ChildController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private static AcademyRepository repository;

        public ChildController()
        {
            repository = new AcademyRepository(db);
        }
        // GET: Children
        public  ActionResult Index()
        {
            return View(repository.GetChildren());
        }

        // GET: Children/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Child child = await db.Children.FindAsync(id);
            if (child == null)
            {
                return HttpNotFound();
            }
            return View(child);
        }

        // GET: Children/Create
        public ActionResult Create()
        {
            ViewBag.AgeGroupID = new SelectList(db.AgeGroups, "AgeGroupID", "Name");
            return View();
        }

        // POST: Children/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChildID,ChildFirstName,ChildLastName,DateOfBirth,PasswordHash,AgeGroupID")] ChildViewModel child)
        {
            string userIdValue="";
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity!=null)
            {
                var userIdClaim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim!=null)
                {
                     userIdValue = userIdClaim.Value;
                }
            }

            if (ModelState.IsValid)
            {
                

                repository.AddChild(new Child { ChildID = child.ChildID ,ChildFirstName=child.ChildFirstName,ChildLastName=child.ChildLastName,DateOfBirth=child.DateOfBirth,PasswordHash=child.PasswordHash,AgeGroupID=child.AgeGroupID,UserID= userIdValue });
                return RedirectToAction("Index");
            }

            ViewBag.AgeGroupID = new SelectList(db.AgeGroups, "AgeGroupID", "Name", child.AgeGroupID);
            return View(child);
        }

        // GET: Children/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Child child = await db.Children.FindAsync(id);
            if (child == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgeGroupID = new SelectList(db.AgeGroups, "AgeGroupID", "Name", child.AgeGroupID);
            return View(child);
        }

        // POST: Children/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ChildID,ChildFirstName,ChildLastName,DateOfBirth,PasswordHash,IsActive,UserID,AgeGroupID")] Child child)
        {
            if (ModelState.IsValid)
            {
                db.Entry(child).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AgeGroupID = new SelectList(db.AgeGroups, "AgeGroupID", "Name", child.AgeGroupID);
            return View(child);
        }

        // GET: Children/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Child child = await db.Children.FindAsync(id);
            if (child == null)
            {
                return HttpNotFound();
            }
            return View(child);
        }

        // POST: Children/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Child child = await db.Children.FindAsync(id);
            db.Children.Remove(child);
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
