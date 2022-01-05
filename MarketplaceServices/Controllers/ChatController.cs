using MarketplaceServices.Data;
using MarketplaceServices.Hubs;
using MarketplaceServices.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MarketplaceServices.Controllers
{



    [Authorize]
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

      


        //Get The Id of the user that i will chat with;
        //Create a chatRoom
        public async Task<ActionResult> CreateRoom(string id)
        {
            //Get the user authentified 
            var thisUser = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var myuserList = new string[]
                   {
                        thisUser ,
                        id,
        
                   };
           
           
                //******Created A Chat Room******//
                ChatRooms room = new ChatRooms()
                {
                    Type = ChatType.Private,

                };
                //Add The first user in the room 
                room.Users.Add(new RoomUser
                {

                    UsersId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier),
                });
                //Add The first user in the room 
                room.Users.Add(new RoomUser
                {

                    UsersId = id,
                });

                {

                }

                _Context.chatrooms.Add(room);

                await _Context.SaveChangesAsync();

                return RedirectToAction("Room", new { id = room.Id });
            

            
           
        }


        public async  Task<ActionResult> Room(string id)
        {



            var thisUser = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            //Retrieve Room Id
            var existRoom = _Context.chatrooms.Find(id);
            //
            if (existRoom != null)
            {
                //******if the user authentified exist in this chatRoom******//
                var user = _Context.roomUsers.Where(u => u.roomsId == id).ToList();
                foreach (var userid in user)
                {
                        if (userid.UsersId.Contains(thisUser))
                        {
                            ViewBag.roomId = id;
                            var room = _Context.chatrooms
                             .Include(x => x.Messages)
                             .SingleOrDefault(x => x.Id == id);

                            return View(room);
                        }
                      
                }

                return NotFound();
                

            }
            return NotFound();
         
           
        }


        // GET: ChatController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendMessage(string text, string roomid)
        {
            if (text != null)
            {
                ChatMessages msg = new ChatMessages();
                msg.Name = User.Identity.Name;
                var sender = await _userManager.GetUserAsync(User);
                msg.Text = text;
                msg.RoomId = roomid;
                msg.time = DateTime.Now;
                await _Context.Messages.AddAsync(msg);
                await _Context.SaveChangesAsync();
                await _hubContext.Clients.Group(roomid).SendAsync("ReceiveMessage", sender.UserName, msg.Text,  msg.time);
                return RedirectToAction("Room", new { id = roomid });
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
