using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarBob.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(1000)]
        public string Status { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } 

        [Required]
        public DateTime FeedbackDate { get; set; }

        public List<string>? Images { get; set; } = new List<string>();

        // ------------- ForeignKey ------------
        public int BookingId { get; set; }
        [ForeignKey("BookingId")]
        [ValidateNever]
        public Booking Booking { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public User User { get; set; }
    }
}
