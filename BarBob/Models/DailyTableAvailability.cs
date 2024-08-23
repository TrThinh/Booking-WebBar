using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarBob.Models
{
    public class DailyTableAvailability
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Range(0, 20, ErrorMessage = "The number of available tables not be 0")]
        public int AvailableTables { get; set; } = 20;

        // -------------------Foreign Key-------------------
        [ForeignKey("Table")]
        public int TableId { get; set; }

        public Table Table { get; set; }
    }
}