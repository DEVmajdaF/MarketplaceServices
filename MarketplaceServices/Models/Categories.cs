using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceServices.Models
{
    public class Categories
    {

        public string Id { get; set; }
        public string CategoryName { get; set; }
        public List<SubCategory> subCategories { get; set; }

    }
}
