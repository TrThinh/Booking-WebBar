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

        public IActionResult Menu(int? id)
        {
            if (id == null || id == 0)
            {
                return View(new Menu());
            }
            Menu menu = _unitOfWork.Menu.Get(t => t.Id == id);
            return View(menu);
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
                tableName = t.Table.Table_name,
                guest = t.Guests,
                bookingDate = t.BookingDate,
                checkinDate = t.CheckinDate,
                checkinTime = t.CheckinTime
            }).ToList();

            return Json(new { data = bookings });
        }

        [HttpGet]
        public IActionResult GetAllMenu()
        {
            var menus = _unitOfWork.Menu.GetAllIncluding().Select(m => new
            {
                type = m.Type,
                name = m.Name,
                description = m.Description,
                price = m.Price,
                id = m.Id
            }).ToList();

            return Json(new { data = menus });
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

        [HttpPost]
        public IActionResult Menu(Menu menu)
        {
            if (ModelState.IsValid)
            {
                if (menu.Id == 0)
                {
                    _unitOfWork.Menu.Add(menu);
                    TempData["success"] = "Item created successfully";
                }
                else
                {
                    _unitOfWork.Menu.Update(menu);
                    TempData["success"] = "Item updated successfully";
                }
                _unitOfWork.Save();
            }
            return RedirectToAction("Menu", "Branch");
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

        [HttpGet]
        public IActionResult GetItemById(int id)
        {
            Menu menu = _unitOfWork.Menu.Get(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return Json(new { data = menu });
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

        [HttpPost]
        public IActionResult CreateItem(Menu menu)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Menu.Add(menu);
                _unitOfWork.Save();
                TempData["success"] = "Item created successfully";
                return RedirectToAction("Menu", "Branch");
            }
            return View(menu);
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

        [HttpDelete]
        public IActionResult DeleteItemById(int id)
        {
            Menu menu = _unitOfWork.Menu.Get(t => t.Id == id);
            if (menu == null)
            {
                return BadRequest(new { success = false, message = "Error while deleting item" });
            }
            _unitOfWork.Menu.Remove(menu);
            _unitOfWork.Save();
            return Ok(new { success = true, message = "Delete item: \"" + menu.Id + "\" successful" });
        }

        #endregion
    }
}
