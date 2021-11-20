using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceServices.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {

        RoleManager<IdentityRole> RoleManager;

        public RoleController(RoleManager<IdentityRole> RoleManager)
        {
            this.RoleManager = RoleManager;
        }
        // GET: RoleController
        public ActionResult Index()
        {
            var Roles = RoleManager.Roles.ToList();
            ViewBag.Roles = Roles;
            return View(Roles);
        }

        // GET: RoleController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RoleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IdentityRole  role)
        {
            var result = await RoleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                TempData["save"] = "Role Has Been Created Succefully";
                return RedirectToAction(nameof(Index));
            }
            return View();


        }

        // GET: RoleController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            ViewBag.Id = role.Id;
            ViewBag.RoleName = role.Name;
            return View(role);
        }

        // POST: RoleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, string roleName)
        {
            var role = await RoleManager.FindByIdAsync(id);
            role.Name = roleName;
            var isExist = await RoleManager.RoleExistsAsync(role.Name);
            if (isExist)
            {
                ViewBag.message = "This role is Already Exist";
                ViewBag.name = roleName;
            }
            var result = await RoleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                ViewBag.message = "Role has been updated successfully";
                return RedirectToAction("Index");
            }
            return View();


        }

        // GET: RoleController/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            ViewBag.RoleName = role.Name;
            return View();
        }

        // POST: RoleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, string name)
        {
            var role = await RoleManager.FindByIdAsync(id);
            var result = await RoleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
