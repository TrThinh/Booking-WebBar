using BarBob.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BarBob.Models.ViewModels
{
    public class ReservationVM
    {
        [Required(ErrorMessage = "Please select a table.")]
        public int TableId { get; set; }

        [Required(ErrorMessage = "Please select a check-in date.")]
        public DateTime CheckinDate { get; set; }

        [Required(ErrorMessage = "Please select a check-in time.")]
        public TimeSpan CheckinTime { get; set; }

        [Required(ErrorMessage = "Please specify the number of guests.")]
        [Range(1, 20, ErrorMessage = "Number of guests must be between 1 and 20.")]
        public int Guests { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> TableList { get; set; }
    }
}