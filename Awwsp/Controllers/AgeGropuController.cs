using Awwsp.Data;
using Awwsp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Awwsp.Controllers
{
    public class AgeGropuController : Controller
    {
        AcademyRepository repository = new AcademyRepository();
        ApplicationDbContext dbContext = new ApplicationDbContext();
        // GET: AgeGropu
        public ActionResult Index()
        {
            var list = repository.GetAgeGroups();
            if (list != null)
            {
                return View(list);

            }
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AgeGroup ageGroup)
        {
            if (ModelState.IsValid)
            {
                repository.AddAgeGroup(ageGroup);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Edit(int id)
        {
            return View(repository.GetAgeGroup(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AgeGroup ageGroup)
        {
            repository.UpdateAgeGroup(ageGroup);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
              var ageGroup = repository.GetAgeGroup(id);
            if (ageGroup == null)
            {
                return HttpNotFound();
            }
            return View(ageGroup);
        }
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            repository.DeleteAgeGroup(id);
            return RedirectToAction("Index");
        }
    }
}