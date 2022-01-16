using MarketplaceServices.Data;
using MarketplaceServices.Models;
using MarketplaceServices.ViewModel.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceServices.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AuthDbContext Context;

        public HomeController(ILogger<HomeController> logger , AuthDbContext context)
        {
            _logger = logger;
            Context = context;
        }

        public IActionResult Index()
        {

            var result = Context.Categories.ToList();
            var services = Context.Services.Include(c=>c.SubCategory).Include(p=>p.Photos).Take(10).ToList();
            HomeViewModel home = new HomeViewModel()
            {
                Services = services,
                Categories = result,
            };
            return View(home);
            
        }

        // POST: 
       
        public ActionResult SearchByCatgory(string CategoryName)
        {
            if(CategoryName != null)
            {
                var services = Context.Services.Include(s => s.SubCategory).Include(p => p.Photos).Where(c=>c.SubCategory.Catgories.CategoryName== CategoryName).ToList();
                return RedirectToAction("GetService", "Home");
            }
            return View("Index");
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FindService(string search)
        {
            List<Services> services = new List<Services>();
 
          if (search != null)
            {
                
                services = Context.Services.Include(s => s.SubCategory).Include(x => x.user).Include(p => p.Photos).Where(c =>c.Title.Contains(search) || c.Description.Contains(search)).ToList();
                var settings = new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                TempData["Services"] = JsonConvert.SerializeObject(services, settings);

                //TempData["Services"] = services.ToList();
                //ViewBag.services = services;
                return RedirectToAction("Index", "Services");
            }
            return View("Index");
        }



            public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //// POST: CategoryController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
