using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceServices.Models
{
    public class RoomUser
    {
        public string UsersId { get; set; }
        public ApplicationUser Users { get; set; }
        public string roomsId { get; set; }
        public ChatRooms rooms { get; set; }
    }
}
