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
    public class ChildController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private static AcademyRepository repository;

        public ChildController()
        {
            repository = new AcademyRepository(db);
        }
        // GET: Children partial view index
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("_IndexChildren", repository.GetChildrenAll(GetUserID()));
            }else return View(repository.GetChildrenAll());
        }

        public ActionResult ChildrenList()
        {
            return View(repository.GetChildrenAll());
        }

        // GET: Children/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Child child = await repository.GetChildById(id);
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
        public ActionResult Create([Bind(Include = "ChildID,ChildFirstName,ChildLastName,DateOfBirth,PasswordHash,AgeGroupID")] ChildCreateVM child,string controller)
        {


            if (ModelState.IsValid)
            {
                var data = DateTime.Now - child.DateOfBirth;
                var age = data.TotalDays / 360;
                if (age > 18 || age < 3)
                {
                    ViewBag.AgeGroupID = new SelectList(db.AgeGroups, "AgeGroupID", "Name", child.AgeGroupID);
                    ModelState.AddModelError("DateOfBirth", "Wiek dziecka jest nie odpowiedni do zapisu w akadami");
                    return View(child);
                }
                var AgeGroups = repository.GetAgeGroups();
                var ageGroupId = AgeGroups.Where(x => x.MinAge <= age && x.MaxAge > age).FirstOrDefault().AgeGroupID;
                repository.AddChild(new Child
                {
                    ChildID = child.ChildID,
                    ChildFirstName = child.ChildFirstName,
                    ChildLastName = child.ChildLastName,
                    DateOfBirth = child.DateOfBirth,
                    PasswordHash = repository.PasswordHash(child.PasswordHash),
                    AgeGroupID = ageGroupId,
                    UserID = GetUserID()
                });
                return RedirectToAction("Index",controller);

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
            Child child = await repository.GetChildById(id);
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
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Child child = await repository.GetChildById(id);
            repository.DeleteChild(id);
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult SignIn()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult SignIn(ChildLoginVM loginVM)
        {

            if (ModelState.IsValid)
            {
                string hash = repository.PasswordHash(loginVM.Password);
                var user = repository.GetChildrenAll().Where(x => x.ChildFirstName == loginVM.FirstName && x.ChildLastName == loginVM.LastName & x.PasswordHash == repository.PasswordHash(loginVM.Password)).FirstOrDefault();
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(loginVM.FirstName, false);
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.Error = "Niepoprawne dane lub brak konta";
            return View();
        }


        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");

        }
        [Authorize(Roles ="Admin,HedCoach,Coach")]
        public ActionResult AcceptApplication() { 
            //int page = 1,string sort = "ChildFirstName",string sortDir = "asc",string search = ""
        //{
        //    int pageSize = 10;
        //    int totalRecord = 0;

        //    if (page < 1) page = 1;

        //    int skip = (page * pageSize) - pageSize;
        //    var data = repository.GetChildren(search,sort,sortDir,skip,pageSize,out totalRecord);
        //    ViewBag.TotalRows = totalRecord;
          var listToAccept = repository.GetChildrenAll().Where(x => x.IsActive == false).Where(x=>x.IsActive==false);
            return View(listToAccept);
        }
        [Authorize(Roles ="Admin,HedCoach,Coach")]

        [HttpPost]
        public ActionResult AcceptApplication(IEnumerable<Child> children)
        {
            foreach (var item in children)
            {
                repository.UpdateChild(item);
            }
            return View();
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
