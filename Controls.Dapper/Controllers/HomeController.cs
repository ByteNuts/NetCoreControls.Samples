using Microsoft.AspNetCore.Mvc;

namespace ByteNuts.NetCoreControls.Samples.Dapper.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Data()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
