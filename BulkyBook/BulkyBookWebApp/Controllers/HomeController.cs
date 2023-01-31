using BulkyBookWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


// HomeController.cs is the controller for the "Home" folder in the View
// it always has to be in the format {Name}Controller.cs and "Name" folder

// when "/home/privacy" is requested by the client, it will come to this file and then look for the "Privacy" method

namespace BulkyBookWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // if this method is executed, it will serve up the HTML/CSS in the "Home" folder, then serve up Index.cshtml
        public IActionResult Index()
        {
            return View();
        }

        // if this method is executed, it will serve up the HTML/CSS in the "Home" folder, then serve up Privacy.cshtml
        // you can go to that cshtml file and change the html to whatever you want
        // funny enough, bootstrap is used as a default in the ASP.NET Core MVC template app
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}