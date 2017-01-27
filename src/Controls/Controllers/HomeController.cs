using ByteNuts.NetCoreControls.Models.Grid;
using Microsoft.AspNetCore.Mvc;

namespace ByteNuts.NetCoreControls.Samples.Controls.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
