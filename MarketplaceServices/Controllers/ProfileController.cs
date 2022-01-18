using MarketplaceServices.Data;
using MarketplaceServices.Models;
using MarketplaceServices.ViewModel;
using MarketplaceServices.ViewModel.Profile;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MarketplaceServices.Repository;
using Microsoft.AspNetCore.Authorization;

namespace MarketplaceServices.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly AuthDbContext Context;
        private readonly UserManager<IdentityUser> UserManager;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IProfile Prof;

        public ProfileController(AuthDbContext context, UserManager<IdentityUser> userManager, IWebHostEnvironment hostEnvironment, IProfile prof)
        {
            Context = context;
            UserManager = userManager;
            _hostEnvironment = hostEnvironment;
            Prof = prof;
        }

        // GET: Profile
        public ActionResult Index()
        {
           
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                ViewBag.userId = userId;
                var user = Context.ApplicationUser.Include(s => s.Skills).Include(L => L.Language).Where(x => x.Id == userId).SingleOrDefault();
                var service = Context.Services.Include(s => s.SubCategory).Include(p => p.Photos).Include(r=>r.Reviews).Where(x => x.userId == userId).ToList();
                var reviews = Context.Reviews.Include(u => u.User).Include(s => s.Service).Where(s => s.Service.userId == userId).ToList();

                ProfileViewModel profile = new ProfileViewModel()
                {
                    User = user,
                    Services = service,
                    Reviews=reviews,

                };
          
          
                return View(profile);
        }


        // POST: Profile/Addskills
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> updateCompleteinfo(ApplicationUser user , IFormFile img)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var app = Context.ApplicationUser.FirstOrDefault(x => x.Id == userId);
            app.Id = userId;
            
            app.FirstName = user.FirstName;
            app.LastName = user.LastName;
            app.Profession = user.Profession;
            app.Address = user.Address;
            app.PhoneNumber = user.PhoneNumber;
            app.Description = user.Description;
            app.UserName = user.UserName;
            await Prof.addimage(img, userId);
            //var result = Context.ApplicationUser.Update(u);

            await Context.SaveChangesAsync();

            return RedirectToAction(nameof(CompleteInformation));
        }








        // POST: Profile/Addskills
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Addskills(Skills skill)
        {
            await Prof.Addskills(skill);
            return RedirectToAction(nameof(Index));
        }

        // POST: Profile/Addskills
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddskillsProfile(Skills skill)
        {
            await Prof.Addskills(skill);
            return Json("Succeess");
        }


        // GET: Profile/Details/5
        public JsonResult getSkills()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Skills = Context.Skills.Where(u => u.UserId == userId).OrderByDescending(t => t.Time).FirstOrDefault();

            return new JsonResult(new { data = Skills });

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> addimage(IFormFile filename, string  UserId )
        {
           
                string fileName = null;
                if (filename != null)
                {
                    //The Root Of the folder (physical path )  wwwroot
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                //fileName = Path.GetFileNameWithoutExtension(filename.FileName);
                fileName = Path.GetFileName(filename.FileName);
                string filepath = Path.Combine(wwwRootPath +"/images/", fileName);
                filename.CopyTo(new FileStream(filepath, FileMode.Create));

                var app = Context.ApplicationUser.FirstOrDefault(x => x.Id == UserId);

                app.Image = fileName;
                await Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
             

            }

            return View("Index");

        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> updateName( string UserId , string UserName )
        {
            if (UserName != null)
            {
                var app = Context.ApplicationUser.FirstOrDefault(x => x.Id == UserId);

                app.UserName = UserName;
                await Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View("Index");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> updateDescription(string UserId, string Description)
        {
            if (Description != null)
            {
                var app = Context.ApplicationUser.FirstOrDefault(x => x.Id == UserId);

                app.Description = Description;
                await Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Index");
        }
        // Get: Profile/deleteSkill/5
     
        public async Task<ActionResult> deleteSkill(string Id )
        {

                if (Id != null)
                {
                await Prof.deleteSkill(Id);
                    return RedirectToAction(nameof(Index));
                }
            
           
            return View("Index");


        }

       //Get
        public async Task<ActionResult> deleteLang(string Id)
        {
            if (Id != null)
            {

                await Prof.deleteLang(Id);
                return RedirectToAction(nameof(Index));
            }


            return View("Index");

        }

        //Get
        public async Task<ActionResult> deleteLangComplete(string Id)
        {
            if (Id != null)
            {
                await Prof.deleteLang(Id);
                return RedirectToAction(nameof(CompleteInformation));
            }
            return View("CompleteInformation");
        }

        public async Task<ActionResult> deleteskillComplete(string Id)
        {
            if (Id != null)
            {
                await Prof.deleteSkill(Id);
                return RedirectToAction(nameof(CompleteInformation));
            }
            return View("CompleteInformation");
        }
        public async Task<ActionResult> deleteExpComplete(string Id)
        {
            if (Id != null)
            {
                await Prof.deleteExperience(Id);
                return RedirectToAction(nameof(CompleteInformation));
            }
            return View("CompleteInformation");
        }

        // POST: Profile/Addskills
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> AddLanguage(Languages language)
        {

            await Prof.AddLanguage(language);
            return RedirectToAction(nameof(Index));
        }


        // POST: Profile/AddLanguage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddLanguagecomplete(Languages language)
        {

            await Prof.AddLanguage(language);
            string message = "SUCCESS";
            return Json(new { Message = message });
        }

        // GET: Profile/Details/5
        public JsonResult getLanguage()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Language = Context.Languages.Where(u => u.UserId == userId).OrderByDescending(t => t.Time).FirstOrDefault();

            return new JsonResult(new { data = Language });

        }



      

        // GET: Profile/Create
        public ActionResult CompleteInformation()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.userid = userId;
            var user = Context.ApplicationUser.Include(s => s.Skills).Include(L => L.Language).Include(e => e.experiences).Where(x => x.Id == userId).SingleOrDefault();
            return View(user);
        }

        // POST: Profile/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddExperience(string Position, string CompanyName, DateTime From, DateTime To)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Experience exp = new Experience();

            exp.Position = Position;
            exp.CompanyName = CompanyName;
            exp.From = From;
            exp.To = To;
            exp.UserId = userId;
            exp.Time = DateTime.Now;

            var result = Context.Experiences.Add(exp);
            await Context.SaveChangesAsync();
            string message = "SUCCESS";
            return Json(new { Message = message });
        }

        // GET: Profile/Details/5
        public JsonResult getExperiences()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var experiences = Context.Experiences.Where(u => u.UserId == userId).OrderByDescending(t=>t.Time).FirstOrDefault();

            return new JsonResult(new { data = experiences });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult>  deleteExperience(string id)
        {
            var result = Context.Experiences.Where(x => x.Id == id).SingleOrDefault();
            Context.Experiences.Remove(result);
            await Context.SaveChangesAsync();
            return NoContent();
        }

        // GET: Profile/Edit/5
        public ActionResult getdeleteexp()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var experiences = Context.Experiences.Where(u => u.UserId == userId);
            return new JsonResult(new { data = experiences });
        }

        // POST: Profile/Edit/5
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

        // GET: Profile/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Profile/Delete/5
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
