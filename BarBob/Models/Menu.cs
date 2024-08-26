using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarBob.Models
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Type name is required")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(18)")]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }
    }
}