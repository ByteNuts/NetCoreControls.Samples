using Microsoft.AspNetCore.Mvc;

namespace ByteNuts.NetCoreControls.Samples.Dapper.Controllers
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
