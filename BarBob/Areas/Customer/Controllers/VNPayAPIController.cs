using BarBob.Areas.Customer.Controllers.Util;
using BarBob.Models.ViewModels;
using BarBob.Repository.IRepository;
using BarBob.Utility.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace BarBob.Areas.Customer.Controllers
{
    [Area("VNPayAPI")]
    public class VNPayAPIController(IUnitOfWork unitOfWork, VNPayService vnPayService) : Controller
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly VNPayService _vnPayService = vnPayService;

        public string url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
        public string returnUrl = $"https://localhost:7278/vnpayAPI/PaymentConfirm";
        public string tmnCode = "Y45HY6IP";
        public string hashSecret = "GXISHAOGGC5Z8YDUKY4EFSJANKSWVP9E";

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SuccessPage()
        {
            return View();
        }

        [Route("Payment")]
        public ActionResult Payment(int bookingId)
        {
            var booking = _unitOfWork.Booking.GetFirstOrDefault(b => b.Id == bookingId);

            if (booking == null)
            {
                return NotFound("Booking null");
            }

            string hostName = System.Net.Dns.GetHostName();
            var ipAddresses = System.Net.Dns.GetHostAddresses(hostName);
            string clientIPAddress = ipAddresses.Length > 0 ? ipAddresses[0].ToString() : "0.0.0.0";

            string paymentUrl = _vnPayService.CreatePaymentUrl(booking, clientIPAddress);

            Console.WriteLine(paymentUrl);

            return Redirect(paymentUrl);
        }

        [Route("/VNPayAPI/PaymentConfirm")]
        public IActionResult PaymentConfirm()
        {
            if (Request.QueryString.HasValue)
            {
                var queryString = Request.QueryString.Value;
                var json = HttpUtility.ParseQueryString(queryString);

                long orderId = Convert.ToInt64(json["vnp_TxnRef"]);
                string orderInfor = json["vnp_OrderInfo"].ToString();
                long vnpayTranId = Convert.ToInt64(json["vnp_TransactionNo"]);
                string vnp_ResponseCode = json["vnp_ResponseCode"].ToString();
                string vnp_SecureHash = json["vnp_SecureHash"].ToString();
                var pos = Request.QueryString.Value.IndexOf("&vnp_SecureHash");

                bool checkSignature = ValidateSignature(Request.QueryString.Value.Substring(1, pos - 1), vnp_SecureHash, hashSecret);
                if (checkSignature && tmnCode == json["vnp_TmnCode"].ToString())
                {
                    if (vnp_ResponseCode == "00")
                    {
                        var booking = _unitOfWork.Booking.GetFirstOrDefault(b => b.Id == orderId);
                        if (booking != null)
                        {
                            booking.Status = "Confirmed";
                            _unitOfWork.Save();
                        }

                        var model = new PaymentSuccessVM
                        {
                            BookingId = (int)orderId,
                            AmountPaid = booking.Count,
                            TransactionId = vnpayTranId.ToString(),
                            PaymentDate = DateTime.Now
                        };

                        return View("SuccessPage", model);
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage");
                    }
                }
                else
                {
                    return RedirectToAction("ErrorPage");
                }
            }
            return RedirectToAction("ErrorPage");
        }

        public bool ValidateSignature(string rspraw, string inputHash, string secretKey)
        {
            string myChecksum = PayLib.HmacSHA512(secretKey, rspraw);
            return myChecksum.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
        }

    }
}
