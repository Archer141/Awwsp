using Awwsp.Data;
using Awwsp.Models;
using Awwsp.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Awwsp.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : BaseController
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;


        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

  
      
        public ActionResult AllUsers()
        {
            var users = UserManager.Users.ToList();

            return View(users);
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
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



        public ActionResult ChangeRole(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userEdit = UserManager.Users.Where(x => x.Id == id).FirstOrDefault();
            if (userEdit != null)
            {
                List<SelectListItem> roleList = new List<SelectListItem>();

                foreach (var item in RoleManager.Roles)
                {
                    roleList.Add(new SelectListItem { Text = item.Name, Value = item.Name });
                }

                ViewBag.Roles = new SelectList(db.Roles, "Id", "Name");

                ChangeRoleVM changeRoleVM = new ChangeRoleVM
                {
                    Id = userEdit.Id,
                    FirstName = userEdit.FirstName,
                    LastName = userEdit.LastName,
                    Email = userEdit.UserName,
                    PreviousRoleName = UserManager.GetRolesAsync(id).Result.FirstOrDefault(),

                };
                return View(changeRoleVM);
            }
            return View();
        }
        [HttpPost]
        public ActionResult ChangeRole(ChangeRoleVM changeRoleVm)
        {
            if (ModelState.IsValid)
            {
                var roleID = RoleManager.FindByName(changeRoleVm.RoleName).Id;

                var userEdit = UserManager.Users.Where(x => x.Id == changeRoleVm.Id).FirstOrDefault();
                var status = UserManager.AddToRole(changeRoleVm.Id, changeRoleVm.RoleName).Succeeded;
                var status2 = UserManager.RemoveFromRole(changeRoleVm.Id, changeRoleVm.PreviousRoleName).Succeeded;
                if (status && status2)
                {
                    //Roles.RemoveUserFromRole(changeRoleVm.Email, changeRoleVm.PreviousRoleName);

                    return RedirectToAction("AllUsers");
                }
                //Roles.AddUserToRole(changeRoleVm.Email, changeRoleVm.RoleName);
                ViewBag.Roles = new SelectList(db.Roles.Where(x => x.Name != "Admin"), "Id", "Name");

                return View(changeRoleVm);
            }
            else
            {
                ViewBag.Roles = new SelectList(db.Roles.Where(x => x.Name != "Admin"), "Id", "Name");

                return View(changeRoleVm);

            }

        }

    }
}
