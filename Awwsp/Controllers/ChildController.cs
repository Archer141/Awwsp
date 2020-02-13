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
using System.Web.Security;

namespace Awwsp.Controllers
{
    [Authorize]
    public class ChildController : BaseController
    {
       
        // GET: Children partial view index
        public ActionResult Index()
        {
            return View(repository.GetChildrenAll(GetUserID()));
        }


        // GET: Children/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Child child = repository.GetChildById(id);
            if (child == null)
            {
                return HttpNotFound();
            }
            return View(child);
        }

        // GET: Children/Create
        public ActionResult Create(string controllerName)
        {
            ViewBag.AgeGroupID = new SelectList(db.AgeGroups, "AgeGroupID", "Name");
            ViewBag.ControllerName = controllerName;
            return View();
        }

        // POST: Children/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChildID,ChildFirstName,ChildLastName,DateOfBirth,PasswordHash,AgeGroupID")] ChildCreateVM child, string controller)
        {


            if (ModelState.IsValid)
            {
                var data = DateTime.Now - child.DateOfBirth;
                var age = data.TotalDays / 365;
                if (age >= 18 || age <= 3)
                {
                    ViewBag.AgeGroupID = new SelectList(db.AgeGroups, "AgeGroupID", "Name", child.AgeGroupID);
               //     ModelState.AddModelError("DateOfBirth", "Wiek dziecka jest nie odpowiedni do zapisu w akadami");
                    ModelState.AddModelError("DateOfBirth", "Unfortunately child age is not appropriate to register in academy");
                    return View(child);
                }
                var AgeGroups = repository.GetAgeGroups();
                var ageGroupId = AgeGroups.Where(x => x.MinAge <= age && x.MaxAge > age).FirstOrDefault().AgeGroupId;
                repository.AddChild(new Child
                {
                    ChildID = child.ChildID,
                    ChildFirstName = child.ChildFirstName,
                    ChildLastName = child.ChildLastName,
                    FullName = child.ChildFirstName+" "+ child.ChildLastName,
                    DateOfBirth = child.DateOfBirth,
                    PasswordHash = repository.PasswordHash(child.PasswordHash),
                    AgeGroupID = ageGroupId,
                    UserID = GetUserID()
                });
                return RedirectToAction("Index", controller);

            }

            ViewBag.AgeGroupID = new SelectList(db.AgeGroups, "AgeGroupID", "Name", child.AgeGroupID);
            return View(child);
        }

        // GET: Children/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Child child = repository.GetChildById(id);
            if (child == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgeGroupID = new SelectList(db.AgeGroups, "AgeGroupID", "Name", child.AgeGroupID);
            return View(child);
        }

        // POST: Children/Edit/5
        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChildID,ChildFirstName,ChildLastName,DateOfBirth,PasswordHash,IsActive,UserID,AgeGroupID")] Child child)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateChild(child);
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
        public ActionResult DeleteConfirmed(int id)
        {
            Child child = repository.GetChildById(id);
            repository.DeleteChild(id);
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(ChildLoginVM loginVM)
        {
            if (loginVM.FirstName != null && loginVM.LastName != null && loginVM.Password != null)
            {
                string hash = repository.PasswordHash(loginVM.Password);
                var user = repository.GetChildrenAll().Where(x => x.ChildFirstName.ToLower() == loginVM.FirstName.ToLower() && x.ChildLastName.ToLower() == loginVM.LastName.ToLower() & x.PasswordHash == repository.PasswordHash(loginVM.Password)).FirstOrDefault();

                if (user != null)
                {
                    if (user.IsActive)
                    {
                        FormsAuthentication.SetAuthCookie(loginVM.FirstName + " " + loginVM.LastName, false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(" ", "Wait for activation");
                        loginVM.Error = "Wait for activation";
                        return View(loginVM);
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Ivnalid password or login");
                    loginVM.Error = "Ivnalid password or login";
                    return View(loginVM);
                }
            }

            ModelState.AddModelError("", "Wrong password or login");
            return View(loginVM);

        }


        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");

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
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
