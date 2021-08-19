using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceServices.Models
{
    public class Skills
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public string Id { get; set; }
        [Required(ErrorMessage = "Please Enter You Skill Name")]
        public string SkillName { get; set; }
        [Required(ErrorMessage = "Please Enter You Skill Level")]

        public string SkillLevel { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}
