using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BarBob.Models
{
    public class BookingRequest
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Required]
        public string Guests { get; set; }
        //-----ForeignKey here------
        public int TableId { get; set; }
        [ForeignKey("TableId")]
        [ValidateNever]
        public Table Table { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public User User { get; set; }
    }
}
