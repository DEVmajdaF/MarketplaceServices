using MarketplaceServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceServices.ViewModel.Profile
{
    public class ProfileViewModel
    {
        public ApplicationUser User { get; set; }
        public List<Services> Services { get; set; }
    }
}
