using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceServices.Models
{
    public class ChatMessages
    {
      
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public string Id { get; set; }
        public string Name { get; set; }
       
        public string Text { get; set; }
        public DateTime  time { get; set; }

        public string RoomId { get; set; }
        public ChatRooms Room { get; set; }

    }
}
