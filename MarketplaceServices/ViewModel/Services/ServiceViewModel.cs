using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceServices.ViewModel
{
    public class ServiceViewModel
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int SalesNumber { get; set; }
        public int Rating { get; set; }
        public string SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
    }
}
