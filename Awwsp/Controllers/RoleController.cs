using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Awwsp.Data;
using Awwsp.Models;

namespace Awwsp.Controllers
{
    public class RoleController : Controller
    {

        private ApplicationRoleManager _roleManager;
        public RoleController() { }
        public RoleController(ApplicationRoleManager roleManager)
        {
            RoleManager = roleManager;
        }

        public ApplicationRoleManager RoleManager
        {
            get { return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>(); }
            private set
            {
                _roleManager = value;
            }
        }
        // GET: Role
        public ActionResult Index() 
        {

            //var list = _repo.GetApplicationRoles();
            //var list2 = RoleManager.Roles;
            List<AcadamyRole> roles = new List<AcadamyRole>();
            foreach (var item in RoleManager.Roles)
            {
                roles.Add(new AcadamyRole(item));
            }
            return View(roles);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(AcadamyRole model)
        {
            var role = new ApplicationRole() { Name = model.Name };
            await RoleManager.CreateAsync(role);
            return RedirectToAction("Index");

        }

        public async Task<ActionResult> Edit(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            return View(new AcadamyRole(role));
        }
        [HttpPost]
        public async Task<ActionResult> Edit(AcadamyRole role)
        {
           
            ApplicationRole a = await RoleManager.FindByIdAsync(role.Id);
            a.Name = role.Name;
            
            await RoleManager.UpdateAsync(a);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            return View(new AcadamyRole(role));
        }
     
        public async Task<ActionResult> Delete(string id)
        {
            var _role = await RoleManager.FindByIdAsync(id);

            return View(new AcadamyRole(_role));
        }
        [HttpPost]
        public async Task<ActionResult> Delete(AcadamyRole role)
        {
            var _role = await RoleManager.FindByIdAsync(role.Id);

            await   RoleManager.DeleteAsync(_role);

            return RedirectToAction("Index");
        }


    }
}