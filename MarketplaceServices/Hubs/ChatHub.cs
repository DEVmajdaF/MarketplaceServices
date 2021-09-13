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

        public async Task SendMessage(string fromuser, Message message)
        {
            //await Clients.
            await Clients.All.SendAsync("receiveMessage",fromuser, message);

            
        }
            
           

    }
}
