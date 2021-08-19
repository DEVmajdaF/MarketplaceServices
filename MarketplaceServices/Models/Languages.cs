using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceServices.Models
{
    public class Languages
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public string Id { get; set; }

        [Required(ErrorMessage = "Please Enter You Language Name")]
        public string LanguageName { get; set; }
        [Required(ErrorMessage = "Please Enter You Language Level")]
        public string LanguageLevel { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
