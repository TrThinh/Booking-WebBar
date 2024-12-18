﻿using Microsoft.AspNetCore.Mvc;
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
using System.Diagnostics;
using BarBob.Repository;


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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ReservationController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
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
                if (reservationVM.CheckinDate.Date < DateTime.Now.Date)
                {
                    ModelState.AddModelError("CheckinDate", "Check-in date must be in the future.");
                    var errorMessages = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    return Json(new { success = false, message = "Invalid booking data.", errors = errorMessages });
                }

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
                    Count = 100000 + reservationVM.Guests * 20000,
                    IsFeedbackGiven = 0
                };

                _unitOfWork.Booking.Add(booking);
                await _unitOfWork.SaveAsync();

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelBooking(int id)
        {
            var booking = await _unitOfWork.Booking.GetFirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            _unitOfWork.Booking.Remove(booking);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "Booking cancelled successfully.";

            return RedirectToAction("History");
        }

        public async Task<IActionResult> Feedback(int id)
        {
            var feedback = new Feedback
            {
                BookingId = id
            };

            return View(feedback);
        }

        public IActionResult FeedbackHistory()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var feedbacks = _unitOfWork.Feedback.GetAll(f => f.UserId == userId);

            return View(feedbacks);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitFeedback(Feedback feedback, List<IFormFile> images)
        {
            feedback.UserId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(feedback.UserId))
            {
                ModelState.AddModelError("UserId", "User ID is required.");
            }

            if (ModelState.IsValid)
            {
                feedback.FeedbackDate = DateTime.Now;

                if (images != null && images.Count <= 5)
                {
                    foreach (var image in images)
                    {
                        if (image.Length > 0)
                        {
                            var fileName = Path.GetFileName(image.FileName);
                            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "feedback", fileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await image.CopyToAsync(stream);
                            }

                            feedback.Images.Add(fileName);
                        }
                    }
                }
                else
                {
                    feedback.Images = null;
                }

                _unitOfWork.Feedback.Add(feedback);

                var booking = await _unitOfWork.Booking.GetFirstOrDefaultAsync(b => b.Id == feedback.BookingId);
                if (booking != null)
                {
                    booking.IsFeedbackGiven = 1;
                    _unitOfWork.Booking.Update(booking);
                }

                await _unitOfWork.SaveAsync();

                TempData["success"] = "Feedback submitted successfully.";
                return RedirectToAction("FeedbackHistory", "Reservation");
            }

            TempData["error"] = "Failed to submit feedback. Please check your input.";
            return View(feedback);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            var feedback = _unitOfWork.Feedback.GetFirstOrDefault(f => f.Id == id);

            if (feedback == null)
            {
                return NotFound();
            }

            _unitOfWork.Feedback.Remove(feedback);
            await _unitOfWork.SaveAsync();

            TempData["success"] = "Feedback deleted successfully.";
            return RedirectToAction(nameof(FeedbackHistory));
        }
    }
}