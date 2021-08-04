using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceServices.Models
{
    public class SubCategory
    {
        public string Id { get; set; }
        public string SubCategoryName { get; set; }
        public string CategoryId { get; set; }
        public Categories Catgories { get; set; }
    }
}
