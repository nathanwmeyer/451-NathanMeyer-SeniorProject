using _451_NathanMeyer_SeniorProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _451_NathanMeyer_SeniorProject.Controllers
{
    // class: HomeController. This class is a controller that displays the home page of the application. The home page is used to navigate to other parts of the application
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

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