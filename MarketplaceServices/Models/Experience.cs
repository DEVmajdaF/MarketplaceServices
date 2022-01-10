using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceServices.Models
{
    public class Experience
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public string Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string CompanyName { get; set; }
        public string Position  { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
