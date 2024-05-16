using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BarBob.Models.ViewModels
{
    public class UserVM
    {
        public User User { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please select a role.")]
        [ValidateNever]
        public string Role { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> RoleList { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Branch Branch { get; set; }
    }
}
