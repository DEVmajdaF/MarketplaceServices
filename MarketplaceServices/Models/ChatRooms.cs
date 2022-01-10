using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceServices.Models
{
    public class ChatRooms
    {

        public ChatRooms()
        {
            this.Users = new List<RoomUser>();
            this.Messages = new List<ChatMessages>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
     
        public string Id { get; set; }
        public string Name { get; set; }
        public ChatType Type { get; set; }
        public DateTime Time { get; set; }
        public List<ChatMessages> Messages { get; set; }
        public List<RoomUser> Users { get; set; }
    }
}
