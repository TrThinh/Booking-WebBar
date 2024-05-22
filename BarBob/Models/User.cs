using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BarBob.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string? FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string? LastName { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        //-------------------------
        [NotMapped]
        public string Role { get; set; }
    }
}
