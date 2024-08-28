using BarBob.Models.ViewModels;
using BarBob.Models;
using BarBob.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BarBob.Data;
using BarBob.Repository.IRepository;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BarBob.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = SD.Role_Customer)]
    public class ReservationController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public ReservationController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
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

        [HttpPost]
        public async Task<IActionResult> BookTable(ReservationVM reservationVM)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid booking data.";
                return RedirectToAction("Index");
            }

            // Get the current user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["Error"] = "User not found.";
                return RedirectToAction("Index");
            }

            // Check table availability
            var availability = _unitOfWork.DailyTableAvailability.Get(
                a => a.TableId == reservationVM.TableId && a.Date == reservationVM.CheckinDate);

            if (availability == null || availability.AvailableTables < reservationVM.Guests)
            {
                TempData["Error"] = "Not enough tables available.";
                return RedirectToAction("Index");
            }

            // Create the booking
            var booking = new Booking
            {
                UserId = user.Id,
                TableId = reservationVM.TableId,
                Guests = reservationVM.Guests,
                BookingDate = DateTime.Now,
                CheckinDate = reservationVM.CheckinDate,
                CheckinTime = reservationVM.CheckinTime
            };

            // Add booking to database
            _unitOfWork.Booking.Add(booking);

            // Update table availability
            availability.AvailableTables -= reservationVM.Guests;
            _unitOfWork.DailyTableAvailability.Update(availability);

            // Save changes to the database
            _unitOfWork.Save();

            TempData["Success"] = "Table booked successfully.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetAvailableTables(DateTime date)
        {
            var availableTables = _unitOfWork.DailyTableAvailability.GetAll(
                a => a.Date == date, includeProperties: "Table");

            return Json(new { data = availableTables });
        }

    }
}
