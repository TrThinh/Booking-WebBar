using Microsoft.AspNetCore.Mvc;

namespace BarBob.Areas.Manager.Controllers
{
    public class HomeManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
