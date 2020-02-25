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
using System.Web.Security;

namespace Awwsp.Controllers
{
    [Authorize(Roles = "Admin,HeadCoach,Coach")]
    public class CoachController : BaseController
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

        // GET: Coach
        public ActionResult Index()
        {
            var model = new NotificationShowVM();
            return View(model);
        }

        [Authorize(Roles = "HeadCoach,Admin")]
        public ActionResult AllCoach()
        {

            var roles = RoleManager.Roles.ToList();
            var coachId = roles.Where(y => y.Name == "Coach").Select(z => z.Id).FirstOrDefault();
            var headCoachId = roles.Where(y => y.Name == "HeadCoach").Select(z => z.Id).FirstOrDefault();
            var users = UserManager.Users.Where(x => x.Roles.FirstOrDefault().RoleId == coachId || x.Roles.FirstOrDefault().RoleId == headCoachId).ToList();

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

            ViewBag.Roles = new SelectList(db.Roles, "Id", "Name");
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
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber.ToString(),
                };

                if (User.IsInRole("Admin"))
                {

                    var result = await UserManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        await UserManager.AddToRoleAsync(user.Id, model.RoleName);
                        // Send an email with this link
                        string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        await UserManager.SendEmailAsync(user.Id, "Welcome coach, Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                        return RedirectToAction("Index", "Coach");

                    }
                    else
                    {
                        ViewBag.Roles = new SelectList(db.Roles, "Id", "Name", model.RoleName);
                    }
                    AddErrors(result);

                }
                else if (User.IsInRole("HeadCoach"))
                {

                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await UserManager.AddToRoleAsync(user.Id, "Coach");
                        string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        await UserManager.SendEmailAsync(user.Id, "Welcome coach, Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                        return RedirectToAction("Index", "Coach");
                    }
                    AddErrors(result);
                }

            }
            // If we got this far, something failed, redisplay form
            return View(model);
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

                ViewBag.Roles = new SelectList(db.Roles.Where(x => x.Name != "Admin"), "Id", "Name");

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

                var status = UserManager.AddToRole(changeRoleVm.Id, changeRoleVm.RoleName);
                var status2 = UserManager.RemoveFromRole(changeRoleVm.Id, changeRoleVm.PreviousRoleName);
                if (status.Succeeded && status2.Succeeded)
                {
                    //Roles.RemoveUserFromRole(changeRoleVm.Email, changeRoleVm.PreviousRoleName);

                    return RedirectToAction("AllCoach");
                }
                //Roles.AddUserToRole(changeRoleVm.Email, changeRoleVm.RoleName);
                ViewBag.Roles = new SelectList(db.Roles.Where(x => x.Name != "Admin"), "Id", "Name");
                AddErrors(status);
                AddErrors(status2);
                return View(changeRoleVm);
            }
            else
            {
                ViewBag.Roles = new SelectList(db.Roles.Where(x => x.Name != "Admin"), "Id", "Name");
                return View(changeRoleVm);

            }

        }


        // GET: Coach/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = UserManager.Users.Where(x => x.Id == id).FirstOrDefault();
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

            var statsu = UserManager.DeleteAsync(user);

            if (statsu.Result.Succeeded)
            {
                return RedirectToAction("AllCoach");
            }
            else
            {
                return View(user);
            }
        }


        public ActionResult AcceptRegistration()
        {
            var model = new AcceptApplicationVM() { PlayersToRegister = GetPlayersToRegister(), };

            return View(model);
        }
        [HttpPost]
        public ActionResult AcceptRegistration(AcceptApplicationVM vM)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in vM.SelectedPlayers)
                {
                    var player = repository.GetChildById(int.Parse(item));
                    player.IsActive = true;
                    repository.UpdateChild(player);

                    repository.AddNotification(new Notification { Title= "Congratulations!", Text= "You got into the academy",ChildId=player.ChildID,AgeGroupId=player.AgeGroupID.Value,Perceived=false });


                }
                return RedirectToAction("Players");
            };
            var model = new AcceptApplicationVM() { PlayersToRegister = GetPlayersToRegister(), };
            return View(model);
        }

        public ActionResult Players() {

            return View(new PlayersVM() { AgeGroups = repository.GetAgeGroups().ToList()});
        
        }

        public ActionResult AssignToTeams()
        {
            foreach (var item in repository.GetChildrenAll().Where(x =>x.IsSignOut == false))
            {
                var data = DateTime.Now - item.DateOfBirth;
                var age = data.TotalDays / 365;

                var AgeGroups = repository.GetAgeGroups();
                item.AgeGroupID = AgeGroups.Where(x => x.MinAge <= age && x.MaxAge > age).FirstOrDefault().AgeGroupId;

                repository.UpdateChild(item);
            }

            return RedirectToAction("Players");
        }



        private IList<SelectListItem> GetPlayersToRegister()
        {
            var list = new List<SelectListItem>();
            foreach (var item in repository.GetChildrenAll().Where(x=>x.IsActive==false&&x.IsSignOut==false))
            {
                list.Add(new SelectListItem { Text = item.FullName, Value = item.ChildID.ToString() });
            }
            return list;
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
