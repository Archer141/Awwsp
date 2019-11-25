using Awwsp.Data;
using Awwsp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Awwsp.Controllers
{
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

            return View();
        }

        // GET: Photo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Photo/Create
        [HttpPost]
        public ActionResult Create(Photo photo, HttpPostedFileBase image1)
        {

            if (ModelState.IsValid && image1 != null)
            {
                repository.AddPhoto(photo,image1);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }

        // GET: Photo/Edit/5
        public ActionResult Edit(int id)
        {
            return View(repository.GetPhotoById(id));
        }

        // POST: Photo/Edit/5
        [HttpPost]
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Photo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
