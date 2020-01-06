using Awwsp.Data;
using Awwsp.Models;
using Awwsp.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Awwsp.Controllers
{
    [Authorize(Roles = "Admin,HedCoah,Coach")]
    public class CoachController : Controller
    {

        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private ApplicationDbContext dbContext = new ApplicationDbContext();
        private static AcademyRepository academyRepository;
        
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

        // GET: Coach
        public ActionResult Index()
        {
            var model = new NotificationShowVM();
            return View(model);
        }

        [Authorize(Roles ="HeadCoach,Admin")]
        public ActionResult AllCoach()
        {

            var roles = RoleManager.Roles.ToList();
            var coachId = roles.Where(y => y.Name == "Coach").Select(z => z.Id).FirstOrDefault();
            var headCoachId = roles.Where(y => y.Name == "HeadCoach").Select(z => z.Id).FirstOrDefault();
            var users = UserManager.Users.Where(x=>x.Roles.FirstOrDefault().RoleId==coachId|| x.Roles.FirstOrDefault().RoleId == headCoachId).ToList();

            return View(users);
        }


        //
        // GET: /Account/Register
        [Authorize(Roles = "HeadCoach,Admin")]
        public ActionResult NewCoach()
        {
            List<SelectListItem> roleList = new List<SelectListItem>();

            foreach (var item in RoleManager.Roles)
            {
                roleList.Add(new SelectListItem { Text = item.Name, Value = item.Name });
            }

            ViewBag.Roles = new SelectList(dbContext.Roles, "Id", "Name");
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [Authorize(Roles = "HeadCoach,Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewCoach(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                if (User.IsInRole("Admin"))
                {
                    var user = new ApplicationUser
                    {
                        UserName = model.Email,
                        Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                    };

                    var result = await UserManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        await UserManager.AddToRoleAsync(user.Id, model.RoleName);
                        // Send an email with this link
                        string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                        return RedirectToAction("Index", "Coach");

                    }
                    else
                    {
                        ViewBag.Roles = new SelectList(dbContext.Roles, "Id", "Name", model.RoleName);
                    }
                    AddErrors(result);

                }
                else if (User.IsInRole("HeadCoach"))
                {
                    var user = new ApplicationUser
                    {
                        UserName = model.Email,
                        Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                    };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await UserManager.AddToRoleAsync(user.Id, "Coach");
                        return RedirectToAction("Index", "Coach");
                    }
                    AddErrors(result);
                }

            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // GET: Coach/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = UserManager.Users.Where(x=>x.Id==id).FirstOrDefault();
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Coach/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var user = UserManager.Users.Where(x => x.Id == id).FirstOrDefault();

            UserManager.DeleteAsync(user);

            return RedirectToAction("Index");
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

    }
}