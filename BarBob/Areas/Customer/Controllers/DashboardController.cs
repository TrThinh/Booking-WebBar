using Microsoft.AspNetCore.Mvc;

namespace BarBob.Areas.Customer.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
