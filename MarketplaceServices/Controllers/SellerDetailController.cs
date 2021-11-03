using MarketplaceServices.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceServices.Controllers
{
    public class SellerDetailController : Controller
    {
        AuthDbContext Context;
        


        public SellerDetailController(AuthDbContext context)
        {
            Context = context;
           
        }
        // GET: SellerDetailController
        public async Task<ActionResult> Index(string id)
        {
            var userinfo = Context.ApplicationUser.Include(s => s.Skills).Include(L => L.Language).Where(x => x.Id == id).SingleOrDefault();
         

            return View(userinfo);
        }

        // GET: SellerDetailController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SellerDetailController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SellerDetailController/Create
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

        // GET: SellerDetailController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SellerDetailController/Edit/5
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

        // GET: SellerDetailController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SellerDetailController/Delete/5
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
