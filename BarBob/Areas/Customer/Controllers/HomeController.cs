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
    }

}
