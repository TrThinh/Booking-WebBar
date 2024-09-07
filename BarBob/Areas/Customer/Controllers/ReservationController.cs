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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookTable(ReservationVM reservationVM)
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
