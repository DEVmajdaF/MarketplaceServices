using MarketplaceServices.Data;
using MarketplaceServices.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceServices.Controllers
{
    public class CategoryController : Controller
    {

        AuthDbContext Context;
        public CategoryController(AuthDbContext context)
        {
            Context = context;
        }
        // GET: CategoryController
        public ActionResult Index()
        {

            var cat = (from c in Context.Categories

                          select new Categories
                          {
                              Id=c.Id,
                              CategoryName = c.CategoryName,
                              subCategories = (from subCatgory in Context.SubCategory
                                               where subCatgory.CatgoriesId == c.Id
                                               select new SubCategory
                                               {
                                                   Id= subCatgory.Id,
                                                   SubCategoryName = subCatgory.SubCategoryName,
                                                   CatgoriesId= c.Id
                                               }
                                               ).ToList()

                          }

                         ).ToList();

          
            return View(cat);
          
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> Create(Categories category)
        {
            if (!ModelState.IsValid)
            {
                Context.Categories.Add(category);
                await Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Index");
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( SubCategory subcat)
        {
           
                if (!ModelState.IsValid)
                {
                    Context.SubCategory.Add(subcat);
                    await Context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
              
          
                return View("Index");
          
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CategoryController/Delete/5
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
