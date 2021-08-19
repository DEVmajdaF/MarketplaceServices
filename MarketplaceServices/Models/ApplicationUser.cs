using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceServices.Models
{
    public class ApplicationUser : IdentityUser
    {

        [PersonalData]
        
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }

        public string Image { get; set; }

        public bool IsOnline { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        [NotMapped]
        [Display(Name = "Profile Picture")]
        public IFormFile ProfileImage { get; set; }

    public DateTime MemberDate { get; set; }
        public List<Languages> Language { get; set; }
        public List<Skills> Skills { get; set; }
        
    }
}

