using Awwsp.Data;
using Awwsp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Awwsp.Controllers
{
    [Authorize]
    public class PhotoController : BaseController
    {
       
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
        [Authorize(Roles ="Admin,HeadCoach,Coach")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Photo/Create
        [Authorize(Roles ="Admin,HeadCoach,Coach")]
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
                ModelState.AddModelError(" ", "Dodaj zdjęcie");
                return View();
            }

        }

        // GET: Photo/Edit/5
        [Authorize(Roles = "Admin,HeadCoach,Coach")]
        public ActionResult Edit(int id)
        {
            return View(repository.GetPhotoById(id));
        }

        // POST: Photo/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin,HeadCoach,Coach")]
        public ActionResult Edit(Photo photo,HttpPostedFileBase image1)
        {
            if (ModelState.IsValid  )
            {
                if (image1 != null)
                {
                 repository.UpdatePhoto(photo, image1);
                 return RedirectToAction("Index");
                }
                else
                {

                    repository.UpdatePhoto(photo);
                    return RedirectToAction("Index");

                }
            }
            else
            {
                
                return View();
            }
        }

        // GET: Photo/Delete/5
        [Authorize(Roles = "Admin,HeadCoach,Coach")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = repository.GetPhotoById(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: Photo/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin,HeadCoach,Coach")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repository.DeletePhoto(id);
            return RedirectToAction("Index");
        }
      

    
    }
}
