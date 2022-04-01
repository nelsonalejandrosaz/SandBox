using Microsoft.AspNetCore.Mvc;

namespace SandBox.Controllers
{
    public class ConfigurationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
