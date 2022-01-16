using MarketplaceServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceServices.ViewModel.SellerProfile
{
    public class SellerDetailViewModel
    {
        public ApplicationUser user { get; set; }
        public List<Services> Services { get; set; }
        public List<Reviews> Reviews { get; set; }
    }
}
