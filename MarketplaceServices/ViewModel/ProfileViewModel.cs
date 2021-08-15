using MarketplaceServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceServices.ViewModel
{
    public class ProfileViewModel
    {

        public ApplicationUser Profile { get; set; }
        public List<Languages> Language { get; set; }
        public List<Skills> Skills { get; set; }
    }
}
