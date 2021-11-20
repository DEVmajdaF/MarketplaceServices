using MarketplaceServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceServices.ViewModel
{
    public class ServiceViewModel
    {
        public Services Service { get; set; }
        public List<Reviews> Reviews { get; set; }
        public ApplicationUser user { get; set; }
    }
}
