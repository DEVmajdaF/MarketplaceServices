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

        public ApplicationUser()
        {
            this.Rooms = new List<RoomUser>();
            
        }

        [PersonalData]
        
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }

        public string Image { get; set; }

        public bool IsOnline { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }
        public string Profession { get; set; }
        


        [NotMapped]
        [Display(Name = "Profile Picture")]
        public IFormFile ProfileImage { get; set; }
        [DisplayFormat(DataFormatString = "{0:Y}", ApplyFormatInEditMode = true)]
        public DateTime MemberDate { get; set; }
        public List<Languages> Language { get; set; }
        public List<Skills> Skills { get; set; }
        public List<Message> Messages { get; set; }
        public List<RoomUser> Rooms { get; set; }
        public List<ChatMessages> Message { get; set; }
        public List<Experience> experiences { get; set; }

    }
}

