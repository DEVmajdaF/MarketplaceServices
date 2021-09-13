using MarketplaceServices.Data;
using MarketplaceServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceServices.Controllers
{
    public class ChatController : Controller
    {


        private readonly AuthDbContext _Context;
        private readonly UserManager<IdentityUser> _userManager;
        public ChatController(AuthDbContext Context, UserManager<IdentityUser> userManager)
        {
            _Context = Context;
            _userManager = userManager;
        }


        // GET: ChatController
        public async  Task<ActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            ViewBag.currentUserName = currentUser.UserName;
            var messages = _Context.Message.ToList();
            return View(messages);

        }

        // GET: ChatController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ChatController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChatController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> Create(string text)
        {
            if (text != null)
            {
                Message msg = new Message();
                msg.UserName = User.Identity.Name;
                var sender = await _userManager.GetUserAsync(User);
                msg.UserId = sender.Id;
                msg.Text = text;
                await _Context.Message.AddAsync(msg);
                await _Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));


            }
            return NotFound();
        }

        // GET: ChatController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ChatController/Edit/5
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

        // GET: ChatController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ChatController/Delete/5
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
