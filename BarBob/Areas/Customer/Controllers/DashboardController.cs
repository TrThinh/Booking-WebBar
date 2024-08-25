using Microsoft.AspNetCore.Mvc;

namespace BarBob.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
