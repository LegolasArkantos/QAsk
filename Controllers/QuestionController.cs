using CrudApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CrudApp.Controllers
{
    public class QuestionController : Controller
    {
        private readonly ILogger<QuestionController> _logger;

        public QuestionController(ILogger<QuestionController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Delete(int id, string userQuestion , string userAnswer)
        {
            var model = new Question
            {
                Id = id,
                UserQuestion = userQuestion,
                UserAnswer = userAnswer
            };
            return View(model);
        }
        public IActionResult Details( int id, string userQuestion,string userAnswer)
        {
            var model = new Question
            {
                Id = id,
                UserQuestion = userQuestion,    
                UserAnswer = userAnswer
            };
            return View(model);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit(int id , string userQuestion, string userAnswer)
        {
            var model = new Question
            {
                Id = id,
                UserQuestion = userQuestion,
                UserAnswer = userAnswer
            };
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
