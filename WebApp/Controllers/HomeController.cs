using Microsoft.AspNetCore.Mvc;

namespace ByteNuts.NetCoreControls.Samples.WebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
