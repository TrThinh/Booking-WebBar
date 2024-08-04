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
        public int TableTypeId { get; set; }
        [ForeignKey("TableTypeId")]
        [ValidateNever]
        public TableType TableType { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
    }
}
