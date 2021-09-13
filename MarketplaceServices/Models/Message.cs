using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceServices.Models
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public string Id { get; set; }
        [Required]  
        public string UserName { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public string UserId { get; set; }
        public  ApplicationUser User { get; set; }
    }
}
