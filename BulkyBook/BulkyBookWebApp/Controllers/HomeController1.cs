using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWebApp.Controllers
{
    public class HomeController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
