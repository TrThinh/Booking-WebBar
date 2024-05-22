using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarBob.Models
{
    public class Slot
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string TablePosition { get; set; }
        [Required]
        public int Status { get; set; }
    }
}
