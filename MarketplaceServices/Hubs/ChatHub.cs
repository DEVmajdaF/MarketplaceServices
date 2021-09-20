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

        //Clients call this to send a message to the hub
        //method to send message to the hub. it will be invoke by the clients 
        public async Task SendMessage(string fromuser, string message)
        {
            //Hub then broadcasts the message to all the connected clients
            //send messages to all clients.

            await Clients.All.SendAsync("receiveMessage", fromuser, message);


        }




    }
}
