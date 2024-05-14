using Microsoft.AspNetCore.Mvc;

namespace BarBob.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
