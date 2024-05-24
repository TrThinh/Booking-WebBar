using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarBob.Models
{
    public class Table
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public TimeSpan Time { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
