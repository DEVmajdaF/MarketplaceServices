using MarketplaceServices.Data;
using MarketplaceServices.ViewModel.SellerProfile;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        public  ActionResult Index(string id)
        {
            var userinfo = Context.ApplicationUser.Include(s => s.Skills).Include(L => L.Language).Where(x => x.Id == id).SingleOrDefault();
            var services = Context.Services.Include(p=>p.Photos).Where(x=>x.userId==id).ToList();
            var reviews = Context.Reviews.Include(u => u.User).Include(s => s.Service).Where(s => s.Service.userId == id).ToList();
            SellerDetailViewModel selerdetail = new SellerDetailViewModel()
            {
                Services= services,
                user=userinfo,
                Reviews=reviews,
            };

            return View(selerdetail);
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
