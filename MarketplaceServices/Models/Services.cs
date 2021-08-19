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
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int SalesNumber { get; set; }
        public List<Reviews> Reviews { get; set; }
        [Required]
        public int Rating { get; set; }
        public List<Photos> Photos { get; set; }
        public string SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
    }
} 
