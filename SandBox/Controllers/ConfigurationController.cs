using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using SandBox.Attributes;
using SandBox.Utilities;

namespace SandBox.Controllers
{
    [PersonalPolicy("Configuracion")]
    public class ConfigurationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
