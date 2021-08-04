﻿using MarketplaceServices.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceServices.Models
{
    public class Reviews
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public string Id { get; set; }
        public string Comment { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime PublishDate { get; set; }
        public string ServiceId { get; set; }
        public Services Service { get; set; }
        public int rating { get; set; }
    }
}
