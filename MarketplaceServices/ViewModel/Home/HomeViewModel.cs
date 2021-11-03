using MarketplaceServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceServices.ViewModel.Home
{
    public class HomeViewModel
    {
        public List<Categories> categories { get; set; }
        //public List<SubCategory> subcategory { get; set; }
        public List<Services> Services { get; set; }
    }
}
