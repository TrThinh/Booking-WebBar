using Microsoft.AspNetCore.Mvc;

namespace BarBob.Areas.Employee.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
