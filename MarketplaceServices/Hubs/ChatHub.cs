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



        //public Task LeaveRoom(string roomId)
        //{
        //    return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
        //}
        //Clients call this to send a message to the hub
        //method to send message to the hub. it will be invoke by the clients 
        //public async Task SendMessageToUser(string Sender, string receiver, string message)
        //{
        //    //Hub then broadcasts the message to all the connected clients
        //    //send messages to all clients.

        //    await Clients.Client(receiver).SendAsync("receiveMessage", Sender, message);
        //}

        public string GetConnectionId() => Context.ConnectionId;


    }
}
