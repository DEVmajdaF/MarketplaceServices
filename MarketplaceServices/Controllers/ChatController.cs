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
            var thisUser = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var allRoomsThisUser = _Context.roomUsers.Where(r=>r.UsersId==thisUser).Select(r=>r.roomsId).ToList();
            var getAllChatRoom = _Context.chatrooms.ToList();
            List<ApplicationUser> getUsersid = new List<ApplicationUser>();

            foreach (var room in allRoomsThisUser)
            {
                var getUsers= _Context.ApplicationUser.Include(p => p.Rooms).Where(p => p.Rooms.Any(a => a.roomsId == room && a.UsersId != thisUser)).FirstOrDefault();
                getUsersid.Add(getUsers);
            }

            return View(getUsersid);
        }

      


        //Get The Id of the user that i will chat with;
        //Create a chatRoom
        public async Task<ActionResult> CreateRoom(string id)
        {
            //Get the user authentified 
            var thisUser = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var checkroom = _Context.roomUsers.ToList();

            RoomUser oldroom = new RoomUser();

            List<RoomUser> checkroomid =  new List<RoomUser> ();

            checkroomid = _Context.roomUsers.Where(r => r.UsersId == id).ToList();

            foreach (var item in checkroom)
            {
                //check if the id in the ckeckroom
                if (id == item.UsersId)
                {

                    foreach (var room1 in checkroomid)
                    {
                       

                        foreach (var users in checkroom)
                        {


                            if (thisUser == users.UsersId && room1.roomsId == users.roomsId)
                            {

                                oldroom = _Context.roomUsers.Where(r => r.UsersId == thisUser && r.roomsId == users.roomsId).FirstOrDefault();
                                break;
                            }
                        }
                    }
                }    
            }


           
            if (oldroom.roomsId != null)
            {
                var roomid = oldroom.roomsId;
                return RedirectToAction("Room", new { id = roomid });
            }

            //******Created A Chat Room******//
            ChatRooms room = new ChatRooms()
            {
                Type = ChatType.Private,
                Time = DateTime.Now,

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

                _Context.chatrooms.Add(room);

                await _Context.SaveChangesAsync();

                return RedirectToAction("Room", new { id = room.Id });
            
           
        }


        public async  Task<ActionResult> Room(string id)
        {

            //Check The Contact
            var thisUser = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var allRoomsThisUser = _Context.roomUsers.Where(r => r.UsersId == thisUser).Select(r => r.roomsId).ToList();
            List<ApplicationUser> getUsersid = new List<ApplicationUser>();

            foreach (var room in allRoomsThisUser)
            {
                var getUsers = _Context.ApplicationUser.Include(p => p.Rooms).Where(p => p.Rooms.Any(a => a.roomsId == room && a.UsersId != thisUser)).FirstOrDefault();
                getUsersid.Add(getUsers);
            }
            var getUser = _Context.ApplicationUser.Include(p => p.Rooms).Where(p => p.Rooms.Any(a => a.roomsId == id && a.UsersId != thisUser)).FirstOrDefault();
            ViewBag.Contacts = getUsersid;
            ViewBag.thisuser = thisUser;
            ViewBag.getuser = getUser;

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
                             .Include(x => x.Messages.OrderBy(t=>t.time)).ThenInclude(p=>p.User)
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

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> SendMessage(string text, string roomid)
        {
            if (text != null)
            {
                var thisUser = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                ChatMessages msg = new ChatMessages();
                msg.Name = User.Identity.Name;
                var sender = await _userManager.GetUserAsync(User);
                msg.Text = text;
                msg.RoomId = roomid;
                msg.time = DateTime.Now;
                msg.UserId= thisUser;
               
                await _Context.Messages.AddAsync(msg);
                await _Context.SaveChangesAsync();
                var img = _Context.ApplicationUser.FirstOrDefault(x => x.Id == thisUser);
                await _hubContext.Clients.Group(roomid).SendAsync("ReceiveMessage", sender.UserName, msg.Text,  msg.time, img.Image );
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
