using Awwsp.Data;
using Awwsp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Awwsp.Controllers
{
    [Authorize]
    public class PhotoController : Controller
    {
        AcademyRepository repository;
        ApplicationDbContext dbContext = new ApplicationDbContext();
        public PhotoController()
        {
            repository = new AcademyRepository(dbContext);
        }
        // GET: Photo
        public ActionResult Index()
        {
            return View(repository.GetPhotos());
        }

        // GET: Photo/Details/5
        public ActionResult Details(int id)
        {
            return View(repository.GetPhotoById(id));
        }

        // GET: Photo/Create
        [Authorize(Roles ="Admin,HedCoach,Coach")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Photo/Create
        [Authorize(Roles ="Admin,HedCoach,Coach")]
        [HttpPost]
        public ActionResult Create(Photo photo, HttpPostedFileBase image1)
        {

            if (ModelState.IsValid && image1 != null)
            {
                photo.Date = DateTime.Now;

                repository.AddPhoto(photo,image1);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Dodaj zdjęcie");
                return View();
            }

        }

        // GET: Photo/Edit/5
        [Authorize(Roles = "Admin,HedCoach,Coach")]
        public ActionResult Edit(int id)
        {
            return View(repository.GetPhotoById(id));
        }

        // POST: Photo/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin,HedCoach,Coach")]
        public ActionResult Edit(Photo photo,HttpPostedFileBase image1)
        {
            if (ModelState.IsValid && image1 != null)
            {
                repository.UpdatePhoto(photo, image1);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        // GET: Photo/Delete/5
        [Authorize(Roles = "Admin,HedCoach,Coach")]
        public ActionResult Delete(int id)
        {
            repository.DeletePhoto(id);
            return RedirectToAction("Index");
        }

 
    }
}
