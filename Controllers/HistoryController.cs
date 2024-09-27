using CrudApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;



namespace CrudApp.Controllers
{
    public class HistoryController : Controller
    {

        private readonly ILogger _logger;

        public HistoryController(ILogger<HistoryController> logger)
        {

            _logger = logger;   
        }


        public IActionResult Index()
        {

            return View();
        }


        public IActionResult Create()
        {

            return View();
        }
    }
}
