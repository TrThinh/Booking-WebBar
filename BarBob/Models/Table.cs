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
        public int TableTypeId { get; set; }
        [ForeignKey("TableTypeId")]
        public TableType TableType { get; set; }
        [Required]
        public string? TableImg {  get; set; }
        [Required]
        public string Description { get; set; }
    }
}
