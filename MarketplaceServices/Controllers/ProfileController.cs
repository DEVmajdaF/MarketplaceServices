using MarketplaceServices.Data;
using MarketplaceServices.Models;
using MarketplaceServices.ViewModel;
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

namespace MarketplaceServices.Controllers
{
    public class ProfileController : Controller
    {
        private readonly AuthDbContext Context;
        private readonly UserManager<IdentityUser> UserManager;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProfileController(AuthDbContext context, UserManager<IdentityUser> userManager, IWebHostEnvironment hostEnvironment)
        {
            Context = context;
            UserManager = userManager;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Profile
        public ActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.userId = userId;
            var user = Context.ApplicationUser.Include(s => s.Skills).Include(L=>L.Language).Where(x => x.Id == userId).SingleOrDefault();
            return View(user);


        }


        // POST: Profile/Addskills
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Addskills(Skills skill)
        {
            Skills Skill = new Skills()
            {
                SkillName = skill.SkillName,
                SkillLevel = skill.SkillLevel
            };
            var result = Context.Add(skill);
            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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

                app.FirstName = UserName;
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
        // POST: Profile/deleteSkill/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> deleteSkill(string Id , Skills skill)
        {

                if (Id != null)
                {
                    var result = Context.Skills.Where(x => x.Id == Id).SingleOrDefault();

                    Context.Skills.Remove(result);
                    await Context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            
           
            return View("Index");


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> deleteLang(string Id)
        {


            if (Id != null)
            {


                var result = Context.Languages.Where(x => x.Id == Id).SingleOrDefault();

                Context.Languages.Remove(result);
                await Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


            return View("Index");

        }










        // POST: Profile/Addskills
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> AddLanguage(Languages language)
        {
            

            Languages languages = new Languages()
            {
                LanguageName = language.LanguageName,
                LanguageLevel = language.LanguageLevel,
                UserId= language.UserId
            };
            var result =  Context.Add(languages);
            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        //Tableau Skills 

        //Tableau Languages 
        // GET: Profile/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Profile/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profile/Create
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

        // GET: Profile/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
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
