using BarBob.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BarBob.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Manager/Home")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Manager/Home/GetBookingsPerTable")]
        public IActionResult GetBookingsPerTable()
        {
            var bookings = _unitOfWork.Booking.GetAll(includeProperties: "Table");

            if (bookings == null || !bookings.Any())
            {
                return Ok(new List<object>());
            }

            var bookingsPerTable = bookings
                .GroupBy(b => new { b.TableId, b.Table.Table_name })
                .Select(g => new
                {
                    TableName = g.Key.Table_name,
                    Count = g.Count()
                })
                .OrderBy(b => b.TableName)
                .ToList();

            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(bookingsPerTable));

            return Ok(bookingsPerTable);
        }

        [HttpGet("Manager/Home/GetBookingSummary")]
        public IActionResult GetBookingSummary()
        {
            var bookings = _unitOfWork.Booking.GetAll(includeProperties: "Table");

            if (bookings == null || !bookings.Any())
            {
                return Ok(new { TotalBookings = 0, TotalPrice = 0 });
            }

            var totalBookings = bookings.Count();
            var totalPrice = bookings.Sum(b => b.Table.Price);

            return Ok(new { totalBookings, totalPrice });
        }

        [HttpGet("Manager/Home/GetBookingConfirm")]
        public IActionResult GetBookingConfirm()
        {
            var bookings = _unitOfWork.Booking.GetAll(includeProperties: "Table");

            if (bookings == null || !bookings.Any())
            {
                return Ok(new { TotalBookingsConfirm = 0, TotalPriceConfirm = 0 });
            }

            var confirmedBookings = bookings.Where(b => b.Status == "Confirmed");

            if (!confirmedBookings.Any())
            {
                return Ok(new { TotalBookingsConfirm = 0, TotalPriceConfirm = 0 });
            }

            var totalBookingsConfirm = confirmedBookings.Count();
            var totalPriceConfirm = confirmedBookings.Sum(b => b.Table.Price);

            return Ok(new { totalBookingsConfirm, totalPriceConfirm });
        }

    }
}
