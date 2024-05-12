using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarBob.Models
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int Discount { get; set; }
        [Required]
        public int Cost { get; set; }
        [Required]
        public int Total { get; set; }
        //-----ForeignKey here-----
        [Required]
        public int DepositId { get; set; }
        [ForeignKey("DepositId")]
        [ValidateNever]
        public Deposit Deposit { get; set; }

        [Required]
        public int ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        [ValidateNever]
        public Service Service { get; set; }
    }
}
