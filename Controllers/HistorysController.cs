using Microsoft.AspNetCore.Mvc;
using CrudApp.Models;
using CrudApp.Data;
using Microsoft.EntityFrameworkCore;


namespace CrudApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class HistorysController:ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public HistorysController(ApplicationDbContext context)
        {
            _context = context;
        }


        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<History>>> GetHistory()
        //{
        //    return await _context.histories.ToListAsync();


        //}

        [HttpPost]
        public async Task<IActionResult> PostHistory([FromBody] History history)
        {
            _context.histories.Add(history);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHistory), new { id = history.Id }, history);

        }


        // GET: api/Historys with filtering
        [HttpGet]
        public async Task<ActionResult<IEnumerable<History>>> GetHistory([FromQuery] string eventName, [FromQuery] int? year, [FromQuery] string country, [FromQuery] string continent)
        {
            var query = _context.histories.AsQueryable();

            // Apply filters dynamically
            if (!string.IsNullOrEmpty(eventName))
            {
                query = query.Where(h => h.EventName.Contains(eventName));
            }

            if (year.HasValue)
            {
                query = query.Where(h => h.Year == year.Value);
            }

            if (!string.IsNullOrEmpty(country))
            {
                query = query.Where(h => h.Country.Contains(country));
            }

            if (!string.IsNullOrEmpty(continent))
            {
                query = query.Where(h => h.Continent.Contains(continent));
            }

            return await query.ToListAsync();
        }



    }
}
