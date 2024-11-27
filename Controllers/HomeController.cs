using Microsoft.AspNetCore.Mvc;

namespace MachineTest.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
