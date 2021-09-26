using MarketplaceServices.Data;
using MarketplaceServices.Hubs;
using MarketplaceServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MarketplaceServices.Controllers
{

    public class ChatController : Controller
    {


        private readonly AuthDbContext _Context;
        private readonly IHubContext<ChatHub> _hubContext;


        private readonly UserManager<IdentityUser> _userManager;
        public ChatController(AuthDbContext Context, UserManager<IdentityUser> userManager, IHubContext<ChatHub> hubContext)
        {
           
            _Context = Context;
            _userManager = userManager;
            _hubContext = hubContext;
        }



        // GET: ChatController
        public async  Task<ActionResult> Index()
        {

            var currentUser = await _userManager.GetUserAsync(User);
            ViewBag.currentUserName = currentUser.UserName;
            var messages = _Context.Message.ToList();
            return View(messages);
          

        }

        // GET: ChatController
        //public async Task<ActionResult> Room()
        //{

        //    var currentUser = await _userManager.GetUserAsync(User);
        //    ViewBag.currentUserName = currentUser.UserName;
        //    var messages = _Context.Message.ToList();
        //    return View(messages);


        //}

        // GET: ChatController

        public async Task<ActionResult> CreateRoom(string id, string connid)
        {

            if (connid == null)
            {
                Console.WriteLine("ma fi connectionId");
                return NotFound();
            }

            ChatRooms room = new ChatRooms()
            {
                Type = ChatType.Private,

            };
            room.Users.Add(new RoomUser {

                UsersId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier),
            });

            room.Users.Add(new RoomUser
            {

                UsersId = id,
            });

          
            _Context.chatrooms.Add(room);
            await _Context.SaveChangesAsync();
            Console.WriteLine(connid);
             await _hubContext.Groups.AddToGroupAsync(connid, room.Id);

            return RedirectToAction("Room", new { id = room.Id });



           }


        public async Task<ActionResult> Room(string id)
        {
           var room = await _Context.chatrooms.Include(x => x.Messages).FirstOrDefaultAsync(x => x.Id == id);
           
            return View(room);


        }


        // GET: ChatController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

       
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> SendMessage(string text)
        //{
        //    if (text != null)
        //    {
        //        Message msg = new Message();
        //        msg.UserName = User.Identity.Name;
        //        var sender = await _userManager.GetUserAsync(User);
        //        msg.UserId = sender.Id;
        //        msg.Text = text;
        //        await _Context.Message.AddAsync(msg);
        //        await _Context.SaveChangesAsync();
        //        await _hubContext.Clients.All.SendAsync("receiveMessage", sender.UserName, text);
        //        return RedirectToAction(nameof(Index));


        //    }
        //    return NotFound();
           
        //}



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendMessage(string text, string roomid)
        {
            if (text != null)
            {
                ChatMessages msg = new ChatMessages();
                msg.Name = User.Identity.Name;
                var sender = await _userManager.GetUserAsync(User);
                msg.RoomId = roomid;
                msg.Text = text;
                msg.time = DateTime.Now;

                await _Context.Messages.AddAsync(msg);
                await _Context.SaveChangesAsync();
                await _hubContext.Clients.Group(roomid).SendAsync("ReceiveMessage", msg.Text, sender.UserName, msg.time);
               
                return RedirectToAction("Room", new { id = roomid });



            }
            return NotFound();

        }



        //[HttpPost("[action]/{ConnectionId}/{roomid}")]
        //public async Task<IActionResult> joinRoom(string ConnectionId , string roomid)
        //{


        //    await  _hubContext.Groups.AddToGroupAsync(ConnectionId, roomid);
        //    return Ok();
        //}

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
