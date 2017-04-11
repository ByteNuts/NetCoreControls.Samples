using Microsoft.AspNetCore.Mvc;

namespace ByteNuts.NetCoreControls.Samples.Ef.Controllers
{
    public class NccHtmlRender : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
