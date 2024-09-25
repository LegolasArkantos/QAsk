using CrudApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CrudApp.Controllers
{
    public class CustomUserController : Controller
    {
        private readonly ILogger<CustomUserController> _logger;

        public CustomUserController(ILogger<CustomUserController> logger)
        {
            _logger = logger;
        }


       
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
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
