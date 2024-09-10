using Microsoft.AspNetCore.Mvc;

namespace BarBob.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OurEvent()
        {
            return View();
        }

        public IActionResult BookEvent()
        {
            return View();
        }

        public IActionResult Menu()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }

}
