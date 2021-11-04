using MarketplaceServices.Data;
using MarketplaceServices.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
            var result = (from c in Context.Categories

                          select new Categories
                          {
                              Id = c.Id,
                              CategoryName = c.CategoryName,
                              subCategories = (from subCatgory in Context.SubCategory
                                               where subCatgory.CatgoriesId == c.Id
                                               select new SubCategory
                                               {
                                                   Id = subCatgory.Id,
                                                   SubCategoryName = subCatgory.SubCategoryName,
                                                   CatgoriesId = c.Id
                                               }
                                               ).ToList()

                          }

                       ).ToList();
            //var result = Context.Categories.Include(x => x.subCategories).Where(c=>x.).ToList();

          
            return View(result);
            
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
    }
}
