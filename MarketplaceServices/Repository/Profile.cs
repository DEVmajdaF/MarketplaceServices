using MarketplaceServices.Data;
using MarketplaceServices.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Security.Claims;

namespace MarketplaceServices.Repository
{

    public class Profile : IProfile
    {
        private readonly AuthDbContext Context;
        private readonly UserManager<IdentityUser> UserManager;
        private readonly IWebHostEnvironment _hostEnvironment;


        public Profile(AuthDbContext context, UserManager<IdentityUser> userManager, IWebHostEnvironment hostEnvironment)
        {
            Context = context;
            UserManager = userManager;
            _hostEnvironment = hostEnvironment;
        }

        public async Task addimage(IFormFile filename, string  userId)
        {

            string fileName = null;
            if (filename != null)
            {
                //The Root Of the folder (physical path )  wwwroot
                string wwwRootPath = _hostEnvironment.WebRootPath;
                //fileName = Path.GetFileNameWithoutExtension(filename.FileName);
                fileName = Path.GetFileName(filename.FileName);
                string filepath = Path.Combine(wwwRootPath + "/images/", fileName);
                filename.CopyTo(new FileStream(filepath, FileMode.Create));

                var app = Context.ApplicationUser.FirstOrDefault(x => x.Id == userId);

                app.Image = fileName;
                await Context.SaveChangesAsync();

            }
           
        }

        public async  Task AddLanguage(Languages language)
        {

            Languages languages = new Languages()
            {
                LanguageName = language.LanguageName,
                LanguageLevel = language.LanguageLevel,
                UserId = language.UserId,
                Time = DateTime.Now,

              
            };
             Context.Add(languages);
            await Context.SaveChangesAsync();
        }

        public async Task Addskills(Skills skil)
        {

            Skills s = new Skills()
            {
                SkillName = skil.SkillName,
                SkillLevel = skil.SkillLevel,
                UserId=skil.UserId,
                Time = DateTime.Now,
            };

            Context.Add(s);
            await Context.SaveChangesAsync();

        }

        public async Task deleteExperience(string id)
        {
           
                var result = Context.Experiences.Where(x => x.Id == id).SingleOrDefault();
                Context.Experiences.Remove(result);
                await Context.SaveChangesAsync(); 
            
        }

        public async Task deleteLang(string Id)
        {
           
                var result = Context.Languages.Where(x => x.Id == Id).SingleOrDefault();
                Context.Languages.Remove(result);
                await Context.SaveChangesAsync();
            
        }

        public async Task deleteSkill(string Id)
        {
            
                var result = Context.Skills.Where(x => x.Id == Id).SingleOrDefault();
                Context.Skills.Remove(result);
                await Context.SaveChangesAsync();
            
        }
    }
}
