using CrudApp.Data;
using CrudApp.Models;
using CrudApp.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CrudApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MessagesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // POST: api/messages
        [HttpPost]
        public async Task<IActionResult> PostMessage([FromBody] Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            message.CreatedAt = DateTime.UtcNow;
            await _unitOfWork.MessageRepository.AddAsync(message);
            await _unitOfWork.SaveAsync();  

            return CreatedAtAction(nameof(PostMessage), new { id = message.Id }, message);
        }

        // GET: api/messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {
            var message = await _unitOfWork.MessageRepository.GetAllAsync();

            return Ok(message);
        }
    }
}
