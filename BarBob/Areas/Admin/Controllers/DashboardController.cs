using BarBob.Repository.IRepository;
using BarBob.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BarBob.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Trả về View cho dashboard
        [HttpGet("Admin/Dashboard/Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Admin/Dashboard/GetBookingsPerTable")]
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

        [HttpGet("Admin/Dashboard/GetBookingSummary")]
        public IActionResult GetBookingSummary()
        {
            var bookings = _unitOfWork.Booking.GetAll(includeProperties: "Table");

            if (bookings == null || !bookings.Any())
            {
                return Ok(new { TotalBookings = 0, TotalPrice = 0 });
            }

            var totalBookings = bookings.Count();
            var totalPrice = bookings.Sum(b => b.Table.Price);

            return Ok(new { totalBookings = totalBookings, totalPrice = totalPrice });
        }
    }
}
