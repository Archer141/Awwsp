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

namespace Awwsp.Controllers
{
    [Authorize]
    public class TrophyController : BaseController
    {
        

        // GET: Trophies
        public ActionResult Index()
        {
            return View(repository.GetTrophies());
        }

        // GET: Trophies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trophy trophy = repository.GetTrophyById(id);
            if (trophy == null)
            {
                return HttpNotFound();
            }
            return View(trophy);
        }

        [Authorize(Roles ="Admin,Coach,HeadCoach")]
        // GET: Trophies/Create
        public ActionResult Create()
        {
            ViewBag.PhotoID = new SelectList(repository.GetPhotos().Where(x => x.IsTrophy == true).ToList(), "PhotoID", "Name");
            return View();
        }

        // POST: Trophies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles ="Admin,Coach,HeadCoach")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrophyID,Name,PhotoID")] Trophy trophy, HttpPostedFileBase image1)
        {
            if (ModelState.IsValid)
            {
                if (image1 != null)
                {
                    repository.AddPhoto(new Photo { Date = DateTime.Now, Name = trophy.Name, IsTrophy = true }, image1);
                    trophy.PhotoID = repository.GetPhotos().Last().PhotoID;
                }

                trophy.Date = DateTime.Now;
                repository.AddTrophy(trophy);
                return RedirectToAction("Index");
            }
            ViewBag.PhotoID = new SelectList(repository.GetPhotos().Where(x => x.IsTrophy == true).ToList(), "PhotoID", "Name");

            return View(trophy);
        }

        // GET: Trophies/Edit/5
        [Authorize(Roles ="Admin,Coach,HeadCoach")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trophy trophy = repository.GetTrophyById(id);
            if (trophy == null)
            {
                return HttpNotFound();
            }
            ViewBag.PhotoID = new SelectList(repository.GetPhotos().Where(x => x.IsTrophy == true).ToList(), "PhotoID", "Name");

            return View(trophy);
        }

        // POST: Trophies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles ="Admin,Coach,HeadCoach")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrophyID,Name,PhotoID")] Trophy trophy, HttpPostedFileBase image1)
        {
            if (ModelState.IsValid)
            {
                if (image1 != null)
                {
                    repository.AddPhoto(new Photo { Date = DateTime.Now, Name = trophy.Name, IsTrophy = true }, image1);
                    trophy.PhotoID = repository.GetPhotos().Last().PhotoID;
                }
                repository.UpdateTrophy(trophy);
                return RedirectToAction("Index");
            }
            ViewBag.PhotoID = new SelectList(repository.GetPhotos().Where(x => x.IsTrophy == true).ToList(), "PhotoID", "Name");

            return View(trophy);
        }

        // GET: Trophies/Delete/5
        [Authorize(Roles ="Admin,Coach,HeadCoach")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trophy trophy = repository.GetTrophyById(id);
            if (trophy == null)
            {
                return HttpNotFound();
            }
            return View(trophy);
        }

        // POST: Trophies/Delete/5
        [Authorize(Roles ="Admin,Coach,HeadCoach")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repository.DeleteTrophy(id);
            return RedirectToAction("Index");
        }


        [Authorize(Roles ="Admin,Coach,HeadCoach")]
        public ActionResult Assign(int id)
        {
            Trophy trophy = repository.GetTrophyById(id);

            return View(new AssignVm { AgeGroups = repository.GetAgeGroups().ToList(), Trophy = trophy });
        }



        [Authorize(Roles ="Admin,Coach,HeadCoach")]
        [Route("Assign")]
        public ActionResult AssignConfirmed(int? trophyId, int? ageGroupId)
        {
            if (trophyId == null || ageGroupId == null)
            {
                return HttpNotFound();
            }
            return View(new AssignConfirmVM
            {
                Trophy = repository.GetTrophyById(trophyId),
                AgeGroup = repository.GetAgeGropuById(ageGroupId)
            });
        }
        [HttpPost, ActionName("AssignConfirmed")]
        [Authorize(Roles ="Admin,Coach,HeadCoach")]
        [ValidateAntiForgeryToken]
        public ActionResult AssignConfirmedd(int trophyId, int ageGroupId)
        {
            var trophy = repository.GetTrophyById(trophyId);
            var ageGroup = repository.GetAgeGropuById(ageGroupId);

            foreach (var item in repository.GetChildrenAll().Where(x => x.IsActive == true).ToList())
            {
                if (item.Trophies.Count() != 0 && trophy.Children.Count() != 0)
                {
                    if (item.Trophies.Where(x => x.TrophyID == trophy.TrophyID).Count() != 0 && trophy.Children.Where(x => x.ChildID == item.ChildID).Count() != 0)
                    {
                        item.Trophies.Add(trophy);
                        trophy.Children.Add(item);
                    }
                    else
                    {
                        TempData["Error"] = "Trophy is already assigned";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    item.Trophies.Add(trophy);
                    trophy.Children.Add(item);
                }

            }
            db.SaveChanges();

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
