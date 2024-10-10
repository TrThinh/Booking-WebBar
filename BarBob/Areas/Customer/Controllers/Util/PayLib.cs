using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace BarBob.Areas.Customer.Controllers.Util
{
    public class PayLib
    {
        private SortedList<string, string> _requestData = new SortedList<string, string>(new VnPayCompare());

        public void AddRequestData(string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _requestData.Add(key, value);
            }
        }

        public string CreateRequestUrl(string baseUrl, string vnp_HashSecret)
        {
            var data = new StringBuilder();
            foreach (KeyValuePair<string, string> kv in _requestData.OrderBy(k => k.Key)) // Sắp xếp theo Key
            {
                if (data.Length > 0)
                {
                    data.Append("&");
                }
                data.Append(kv.Key + "=" + kv.Value);
            }

            string rawData = data.ToString();

            // Tạo vnp_SecureHash
            string vnp_SecureHash = HmacSHA512(rawData, vnp_HashSecret);

            // Tạo URL thanh toán
            var paymentUrl = new StringBuilder(baseUrl);
            paymentUrl.Append("?" + rawData);
            paymentUrl.Append("&vnp_SecureHash=" + vnp_SecureHash);

            return paymentUrl.ToString();
        }

        public static string HmacSHA512(string key, string inputData)
        {
            var hash = new StringBuilder();
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2")); // Mã hóa thành chuỗi hex
                }
            }

            return hash.ToString();
        }
    }

    public class VnPayCompare : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (x == y) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            var vnpCompare = CompareInfo.GetCompareInfo("en-US");
            return vnpCompare.Compare(x, y, CompareOptions.Ordinal);
        }
    }

}