using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using BarBob.Areas.Customer.Controllers.Util;
using BarBob.Models;
using System.Security.Claims;
using BarBob.Models.ViewModels;
using BarBob.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using BarBob.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Web;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;


namespace BarBob.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = SD.Role_Customer)]
    public class ReservationController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public ReservationController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
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
                    TableId = reservationVM.TableId,
                    Guests = reservationVM.Guests,
                    BookingDate = DateTime.Now,
                    CheckinDate = reservationVM.CheckinDate,
                    CheckinTime = reservationVM.CheckinTime,
                    Status = "Pending",
                    Count = 100000
                };

                _unitOfWork.Booking.Add(booking);
                await _unitOfWork.SaveAsync();

                // Redirect đến trang History khi booking thành công
                return RedirectToAction("History", "Reservation");
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


        public IActionResult History()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var bookings = _unitOfWork.Booking.GetAll(b => b.UserId == userId, includeProperties: "Table");

            return View(bookings);
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

        public IActionResult Pay(int bookingId)
        {
            var booking = _unitOfWork.Booking.GetFirstOrDefault(b => b.Id == bookingId, includeProperties: "Table");
            if (booking == null)
            {
                return NotFound();
            }

            var vnp_ReturnUrl = _configuration["VNPay:vnp_ReturnUrl"]; // URL trả về sau khi thanh toán
            var vnp_Url = _configuration["VNPay:vnp_Url"]; // URL VNPay API
            var vnp_TmnCode = _configuration["VNPay:vnp_TmnCode"];
            var vnp_HashSecret = _configuration["VNPay:vnp_HashSecret"];

            PayLib vnPay = new PayLib();
            vnPay.AddRequestData("vnp_Version", "2.1.0");
            vnPay.AddRequestData("vnp_Command", "pay");
            vnPay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnPay.AddRequestData("vnp_Amount", ((int)booking.Count * 100).ToString()); // Số tiền (VND)
            vnPay.AddRequestData("vnp_CurrCode", "VND");
            vnPay.AddRequestData("vnp_TxnRef", booking.Id.ToString()); // Mã giao dịch
            vnPay.AddRequestData("vnp_OrderInfo", $"Thanh toan booking {booking.Id}");
            vnPay.AddRequestData("vnp_OrderType", "other");
            vnPay.AddRequestData("vnp_Locale", "vn"); // Tiếng Việt
            vnPay.AddRequestData("vnp_ReturnUrl", vnp_ReturnUrl);
            vnPay.AddRequestData("vnp_IpAddr", Request.HttpContext.Connection.RemoteIpAddress?.ToString());
            vnPay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));

            // Tạo chuỗi VNPAY URL + SecureHash
            string paymentUrl = vnPay.CreateRequestUrl(vnp_Url, vnp_HashSecret);

            return Redirect(paymentUrl);
        }

        private string HmacSHA512(string key, string data)
        {
            using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key)))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                return string.Concat(hash.Select(b => b.ToString("x2")));
            }
        }

        public IActionResult PaymentConfirm()
        {
            var vnp_HashSecret = _configuration["VNPay:vnp_HashSecret"];

            // Lấy dữ liệu trả về từ VNPay
            var vnp_SecureHash = Request.Query["vnp_SecureHash"];
            var queryParams = Request.Query.ToDictionary(k => k.Key, v => v.Value.ToString());

            // Kiểm tra hash và trạng thái thanh toán
            var bookingId = int.Parse(Request.Query["vnp_TxnRef"]);
            var booking = _unitOfWork.Booking.GetFirstOrDefault(b => b.Id == bookingId);
            if (booking == null)
            {
                return NotFound();
            }

            var vnp_ResponseCode = Request.Query["vnp_ResponseCode"];
            if (vnp_ResponseCode == "00")
            {
                booking.Status = "Paid";
                _unitOfWork.Save();
                return RedirectToAction("History");
            }
            else
            {
                return Content($"Payment failed with response code: {vnp_ResponseCode}");
            }
        }

    }
}