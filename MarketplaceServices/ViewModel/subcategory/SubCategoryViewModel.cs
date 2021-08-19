using MarketplaceServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceServices.ViewModel.subcategory
{
    public class SubCategoryViewModel
    {
        public string SubCategoryName { get; set; }
        public string CatgoriesId { get; set; }
        public string CatgoriesName { get; set; }
        public List<SubCategory> subCategories { get; set; }

    }
}
