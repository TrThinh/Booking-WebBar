using Microsoft.AspNetCore.Mvc.Rendering;

namespace BarBob.Models.ViewModels
{
    public class ReservationVM
    {
        public int TableId { get; set; }
        public DateTime CheckinDate { get; set; }
        public TimeSpan CheckinTime { get; set; }
        public int Guests { get; set; }
        public IEnumerable<SelectListItem> TableList { get; set; }
    }
}
