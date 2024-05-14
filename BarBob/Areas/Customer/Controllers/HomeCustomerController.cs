using Microsoft.AspNetCore.Mvc;

namespace BarBob.Areas.Customer.Controllers
{
    public class HomeCustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
