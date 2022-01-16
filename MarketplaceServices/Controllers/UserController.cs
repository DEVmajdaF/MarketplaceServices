using MarketplaceServices.Data;
using MarketplaceServices.Models;
using MarketplaceServices.Repository;
using MarketplaceServices.ViewModel;
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

    public class UserController : Controller
    {
        AuthDbContext Context;
        UserManager<IdentityUser> userManager;
        RoleManager<IdentityRole> roleManager;
        private readonly IProfile Prof;
        public readonly IPasswordHasher<IdentityUser> _passwordHasher;



        public UserController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, AuthDbContext Context, IProfile prof, IPasswordHasher<IdentityUser> _passwordHasher)
        {
            this.userManager = userManager;
            this.roleManager= roleManager;
            this.Context = Context;
            Prof = prof;
            this._passwordHasher = _passwordHasher;
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

                               );

        

            return View(userwithrole);
        }

       

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAdmin(ApplicationUser user, IFormFile img)
        {
            if(user != null)
            {
                ApplicationUser s = new ApplicationUser();
                s.FirstName = user.FirstName;
                s.LastName = user.LastName;
                s.Email = user.Email;
                s.NormalizedUserName = user.Email;
                s.Profession = user.Profession;
                s.Address = user.Address;
               s.PhoneNumber = user.PhoneNumber;
               s.Description = user.Description;
                s.UserName = user.UserName;
                Context.ApplicationUser.Add(s);
                var hashedPassword = _passwordHasher.HashPassword(s, "YourPassword");
                s.SecurityStamp = Guid.NewGuid().ToString();
                s.PasswordHash = hashedPassword;
                await Context.SaveChangesAsync();
                await userManager.AddToRoleAsync(s, "Admin");

                return RedirectToAction(nameof(Index));
            }
            return NoContent();
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(string Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var user =  Context.ApplicationUser.Find(Id);
            if(user == null)
            {
                return NotFound();
            }


            return View(user);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string Id, ApplicationUser user)
        {
            if(Id != user.Id)
            {
                return NotFound();
            }

            var users =  Context.ApplicationUser.FirstOrDefault(u =>u.Id == user.Id);
            if (ModelState.IsValid)
            {
                users.FirstName = user.FirstName;
                users.LastName = user.LastName;
                users.PhoneNumber = user.PhoneNumber;
                users.Email = user.Email;

                Context.ApplicationUser.Update(users);
                await Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            
            return View();

        }

        // GET: UserController/Delete/5
        public async Task<ActionResult> Delete(string Id)
        {
            var User = await Context.ApplicationUser.FindAsync(Id);
            if (User == null)
            {
                return NotFound();
            }
            return View(User);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <ActionResult> Delete(string Id, IFormCollection collection)
        {
            var User = await Context.ApplicationUser.FindAsync(Id);
            if (User == null)
            {
                return NotFound();
            }

            var result = await userManager.DeleteAsync(User);
            if (!result.Succeeded)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }


        // GET: UserController/Delete/5
        public async Task<ActionResult> ManageUserRoles(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

         
            if (user == null)
            {
                return NotFound();
            }
            ViewBag.userId = userId;
            //Declarer Une Liste qui se compose de RoleId, RoleName, IsSelected
            var model = new List<EditUserRoleViewModel>();

            foreach (var role in roleManager.Roles.ToList())
            {
                var EditUserRoleViewModel = new EditUserRoleViewModel()
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if ( await userManager.IsInRoleAsync(user,role.Name))
                {
                    EditUserRoleViewModel.IsSelected = true;
                }
                else
                {
                    EditUserRoleViewModel.IsSelected = false;
                }

                model.Add(EditUserRoleViewModel);
            };
            return View(model);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ManageUserRoles(string userId, List<EditUserRoleViewModel> model)
        {
            var user = await userManager.FindByIdAsync(userId);
         
            if (user == null)
            {
                return NotFound();
            }
         
            var roles = await userManager.GetRolesAsync(user);
            var result = await userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                return View(model);
            }

            result = await userManager.AddToRolesAsync(user, model.Where(x=>x.IsSelected).Select(y=>y.RoleName));

            if (!result.Succeeded)
            {
                return View(model);
            }
            return RedirectToAction("Edit", new { id=userId});

        }
    }
}
