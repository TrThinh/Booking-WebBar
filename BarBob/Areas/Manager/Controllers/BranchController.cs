using BarBob.Data;
using BarBob.Models;
using BarBob.Repository.IRepository;
using BarBob.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarBob.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = SD.Role_Manager)]
    public class BranchController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        public BranchController(IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public IActionResult Index(int? id)
        {
            if (id == null || id == 0)
            {
                return View(new Table());
            }
            Table table = _unitOfWork.Table.Get(t => t.Id == id);
            return View(table);
        }

        public IActionResult Booking(int? id)
        {
            if (id == null || id == 0)
            {
                return View(new Booking());
            }
            Booking booking = _unitOfWork.Booking.Get(t => t.Id == id);
            return View(booking);
        }

        public IActionResult BookingConfirmed(int? id) 
        {
            if (id == null || id == 0)
            {
                return View(new Booking());
            }
            Booking booking = _unitOfWork.Booking.Get(t => t.Id == id);
            return View();
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var tables = _unitOfWork.Table.GetAllIncluding().Select(t => new
            {
                table_name = t.Table_name,
                description = t.Description,
                price = t.Price,
                id = t.Id
            }).ToList();

            return Json(new { data = tables });
        }

        [HttpGet]
        public IActionResult GetAllBooking()
        {
            var bookings = _unitOfWork.Booking.GetAllIncluding(b => b.User, b => b.Table).Select(t => new
            {
                userName = t.User.UserName,
                phoneNumber = t.User.PhoneNumber,
                tableName = t.Table.Table_name,
                guest = t.Guests,
                bookingDate = t.BookingDate,
                checkinDate = t.CheckinDate,
                checkinTime = t.CheckinTime,
                status = t.Status
            }).ToList();

            return Json(new { data = bookings });
        }

        [HttpGet]
        public IActionResult GetBookingConfirmed()
        {
            var bookings = _unitOfWork.Booking.GetAllIncluding(b => b.User, b => b.Table)
                .Where(b => b.Status == "Confirmed")
                .Select(t => new
                {
                    userName = t.User.LastName,
                    phoneNumber = t.User.PhoneNumber,
                    tableName = t.Table.Table_name,
                    guest = t.Guests,
                    checkinDate = t.CheckinDate,
                    checkinTime = t.CheckinTime,
                    status = t.Status
                }).ToList();

            Console.WriteLine("Bookings : " + bookings.Count);
            return Json(new { data = bookings });
        }

        [HttpPost]
        public IActionResult Index(Table table)
        {
            if (ModelState.IsValid)
            {
                if (table.Id == 0)
                {
                    _unitOfWork.Table.Add(table);
                    TempData["success"] = "Table created successfully";
                }
                else
                {
                    _unitOfWork.Table.Update(table);
                    TempData["success"] = "Table updated successfully";
                }
                _unitOfWork.Save();
            }
            return RedirectToAction("Index", "Branch");
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            Table table = _unitOfWork.Table.Get(t => t.Id == id);
            if (table == null)
            {
                return NotFound();
            }

            return Json(new { data = table });
        }

        [HttpPost]
        public IActionResult CreateTable(Table table)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Table.Add(table);
                _unitOfWork.Save();
                TempData["success"] = "Dinner created successfully";
                return RedirectToAction("Index", "Branch");
            }
            return View(table);
        }

        [HttpDelete]
        public IActionResult DeleteById(int id)
        {
            Table table = _unitOfWork.Table.Get(t => t.Id == id);
            if (table == null)
            {
                return BadRequest(new { success = false, message = "Error while deleting table" });
            }
            _unitOfWork.Table.Remove(table);
            _unitOfWork.Save();
            return Ok(new { success = true, message = "Delete table: \"" + table.Id + "\" successful" });
        }

        #endregion
    }
}
