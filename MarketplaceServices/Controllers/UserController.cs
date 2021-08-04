using MarketplaceServices.Data;
using MarketplaceServices.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceServices.Controllers
{
    
   

    public class UserController : Controller
    {
        AuthDbContext Context;
        UserManager<IdentityUser> userManager;
        RoleManager<IdentityRole> roleManager;


        public UserController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, AuthDbContext Context)
        {
            this.userManager = userManager;
            this.roleManager= roleManager;
            this.Context = Context;
        }
        // GET: UserController
        public ActionResult Index()
        {
            var userwithrole = (from us in Context.UserRoles
                                join role in Context.Roles on us.RoleId equals role.Id
                                join user in Context.Users on us.UserId equals user.Id

                                select  new UserRoleViewModel()
                                {
                                    Id = user.Id,
                                    Email = user.UserName,
                                    RoleName = role.Name,

                                }

                               );;

            return View(userwithrole);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
