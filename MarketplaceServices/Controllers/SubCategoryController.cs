using MarketplaceServices.Data;
using MarketplaceServices.ViewModel.subcategory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceServices.Controllers
{
    public class SubCategoryController : Controller
    {
        AuthDbContext Context;
        public SubCategoryController(AuthDbContext context)
        {
            Context = context;
        }
        // GET: SubCategoryController
        public ActionResult Index()
        {
          
                         
            return View();
        }

        // GET: SubCategoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SubCategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SubCategoryController/Create
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

        // GET: SubCategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SubCategoryController/Edit/5
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

        // GET: SubCategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SubCategoryController/Delete/5
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
