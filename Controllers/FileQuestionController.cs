using CrudApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CrudApp.Controllers
{
    public class FileQuestionController : Controller
    {
        private readonly ILogger<FileQuestionController> _logger;

        public FileQuestionController(ILogger<FileQuestionController> logger)
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

        public IActionResult AddAnswer(int Id)
        {
            var model = new Answer { FileQuestionId = Id };
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
