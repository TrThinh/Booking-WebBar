using BarBob.Areas.Customer.Controllers.Util;
using BarBob.Models;

namespace BarBob.Utility.Service
{
    public class VNPayService
    {
        private readonly PayLib _payLib;

        public VNPayService(IHttpContextAccessor httpContextAccessor)
        {
            _payLib = new PayLib(httpContextAccessor);
        }

        public string CreatePaymentUrl(Booking booking, string clientIPAddress)
        {
            string tmnCode = "Y45HY6IP";
            string hashSecret = "GXISHAOGGC5Z8YDUKY4EFSJANKSWVP9E";
            string returnUrl = "https://localhost:7278/vnpayAPI/PaymentConfirm";

            _payLib.AddRequestData("vnp_Version", "2.1.0");
            _payLib.AddRequestData("vnp_Command", "pay");
            _payLib.AddRequestData("vnp_TmnCode", tmnCode);
            _payLib.AddRequestData("vnp_Amount", ((int)(booking.Count * 100)).ToString());
            _payLib.AddRequestData("vnp_BankCode", "");
            _payLib.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            _payLib.AddRequestData("vnp_CurrCode", "VND");
            _payLib.AddRequestData("vnp_IpAddr", clientIPAddress);
            _payLib.AddRequestData("vnp_Locale", "vn");
            _payLib.AddRequestData("vnp_OrderInfo", $"Thanh toán cho BookingId: {booking.Id}");
            _payLib.AddRequestData("vnp_OrderType", "other");
            _payLib.AddRequestData("vnp_ReturnUrl", returnUrl);
            _payLib.AddRequestData("vnp_TxnRef", booking.Id.ToString());

            return _payLib.CreateRequestUrl("https://sandbox.vnpayment.vn/paymentv2/vpcpay.html", hashSecret);
        }
    }
}
