using Microsoft.AspNetCore.Mvc;

namespace BarBob.Areas.Manager.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
