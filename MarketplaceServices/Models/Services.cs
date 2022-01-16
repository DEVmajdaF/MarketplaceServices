using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceServices.Models
{
    public class Services
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public string Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [MinLength(80)]
        public string Title { get; set; }
        [Required]
        [MinLength(200)]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        public int SalesNumber { get; set; }
        public int Rating { get; set; }
        public List<Reviews> Reviews { get; set; }
        public List<Photos> Photos { get; set; }
        public string SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
        public string userId { get; set; }
        public ApplicationUser user { get; set; }
    }
} 
