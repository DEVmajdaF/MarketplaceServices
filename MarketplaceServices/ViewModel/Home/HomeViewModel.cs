using MarketplaceServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceServices.ViewModel.Home
{
    public class HomeViewModel
    {
        public ApplicationUser user { get; set; }
        public List<Services> Services { get; set; }
        public List<Categories> Categories { get; set; }


    }
}
