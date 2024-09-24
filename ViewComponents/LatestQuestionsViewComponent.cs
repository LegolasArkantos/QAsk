using CrudApp.Data;
using CrudApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CrudApp.ViewComponents
{
    public class LatestQuestionsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public LatestQuestionsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int numberOfQuestions = 2)
        {
            var latestQuestions = await _context.Question
                .OrderByDescending(q => q.Id)
                .Take(numberOfQuestions)
                .ToListAsync();

            return View(latestQuestions);
        }
    }
}
