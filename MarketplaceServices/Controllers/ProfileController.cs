using MarketplaceServices.Data;
using MarketplaceServices.Models;
using MarketplaceServices.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MarketplaceServices.Controllers
{
    public class ProfileController : Controller
    {
        private readonly AuthDbContext Context;
        private readonly UserManager<IdentityUser> UserManager;

        public ProfileController(AuthDbContext context, UserManager<IdentityUser> userManager )
        {
            Context = context;
            UserManager = userManager;
        }

        // GET: Profile
        public ActionResult Index()
        {
            //Information
            //userId = Context.ApplicationUsers.FirstOrDefault().Id;
           //var  userId = UserManager.GetUserId(User);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.userId = userId;
            var userinfo = (from us in Context.ApplicationUser
                            where us.Id == userId
                            select    new ApplicationUser
                            {

                                FirstName = us.FirstName,
                                LastName = us.LastName,
                                MemberDate = us.MemberDate,
                                Description = us.Description
                                
                            }).SingleOrDefault();

            var SkillInfo = (from S in Context.Skills
                             where S.User.Id == userId
                             select new Skills
                             { SkillName = S.SkillName, 
                               SkillLevel=S.SkillLevel
                             }).ToList();


            var LanguageInfo = (from L in Context.Languages
                             where L.User.Id == userId
                             select new Languages
                             {
                                 LanguageName = L.LanguageName,
                                 LanguageLevel = L.LanguageLevel
                             }).ToList();

            ProfileViewModel profileViewModel = new ProfileViewModel()
            {
                Profile = userinfo,
                Skills = SkillInfo,
                Language= LanguageInfo
            };



            return View(profileViewModel);
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
