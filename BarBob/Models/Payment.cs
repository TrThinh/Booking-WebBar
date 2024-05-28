using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarBob.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string First_name { get; set; }
        [Required]
        public string Last_name { get; set;}
        [Required]
        public string Credit_card_no { get; set; }
        [Required]
        public DateTime Expire_date { get; set; }
        //-----ForeignKey here------
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
