using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BarBob.Models.ViewModels
{
    public class ReservationVM
    {
        public DateTime CheckinDate { get; set; }
        public int Guests { get; set; }
        public TimeSpan CheckinTime { get; set; }

        public int TableId { get; set; }
        public IEnumerable<SelectListItem> TableList { get; set; }
    }
}
