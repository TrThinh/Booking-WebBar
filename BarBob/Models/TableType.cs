using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarBob.Models
{
    public class TableType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Table_name { get; set; }
        [Required]
        public string Price { get; set; }
        
    }
}
