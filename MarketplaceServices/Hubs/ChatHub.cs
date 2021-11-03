using MarketplaceServices.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceServices.Hubs
{
    public class ChatHub : Hub
    {

        public async Task JoinGroup(string roomName)
        {

          
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
           await  Clients.Group(roomName).SendAsync(Context.User.Identity.Name + " joined.");
        }

        public string GetConnectionId() => Context.ConnectionId;


    }
}
