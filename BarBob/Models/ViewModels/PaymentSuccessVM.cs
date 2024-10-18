using BarBob.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BarBob.Models.ViewModels
{
    public class PaymentSuccessVM
    {
        public int BookingId { get; set; }
        public decimal AmountPaid { get; set; }
        public string TransactionId { get; set; }
        public DateTime PaymentDate { get; set; }
    }

}