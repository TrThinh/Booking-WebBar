using Microsoft.AspNetCore.Mvc;

namespace BarBob.Areas.Employee.Controllers
{
    public class HomeEmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
