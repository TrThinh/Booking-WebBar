using BarBob.Models;
using BarBob.Models.ViewModels;
using BarBob.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Security.Claims;

namespace BarBob.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OurEvent()
        {
            return View();
        }
        public IActionResult BookEvent()
        {
            return View();
        }

        public IActionResult Reservation()
        {
            var tables = _unitOfWork.Table.GetAll();

            var reservationVM = new ReservationVM
            {
                TableList = tables.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = $"{t.Table_name} - {t.Description}"
                }).ToList()
            };

            return View(reservationVM);
        }

        public IActionResult Menu()
		{
			return View();
		}

		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        //Reservation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reservation(ReservationVM reservationVM)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var booking = new Booking
                {
                    UserId = userId,
                    TableId = (int)reservationVM.TableId,
                    Guests = reservationVM.Guests,
                    BookingDate = DateTime.Now,
                    CheckinDate = reservationVM.CheckinDate,
                    CheckinTime = reservationVM.CheckinTime
                };

                _unitOfWork.Booking.Add(booking);
                await _unitOfWork.SaveAsync();

                return Json(new { success = true, message = "Booking table successfully." });
            }
            else
            {
                var errorMessages = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return Json(new { success = false, message = "Invalid booking data.", errors = errorMessages });
            }
        }

        private IEnumerable<SelectListItem> GetAvailableTables()
        {
            var tables = _unitOfWork.Table.GetAll();
            return tables.Select(t => new SelectListItem
            {
                Text = t.Table_name + " - " + t.Description,
                Value = t.Id.ToString()
            });
        }
    }
}
