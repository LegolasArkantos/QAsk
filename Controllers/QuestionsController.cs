using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CrudApp.Models;
using CrudApp.UnitOfWork;

namespace CrudApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuestionsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Questions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()
        {
            var questions = await _unitOfWork.QuestionRepository.GetAllAsync();
            return Ok(questions);
        }

        // GET: api/Questions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestion(int id)
        {
            var question = await _unitOfWork.QuestionRepository.GetByIdAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }

        // POST: api/Questions
        [HttpPost]
        public async Task<ActionResult<Question>> PostQuestion([FromBody] Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _unitOfWork.QuestionRepository.AddAsync(question);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction(nameof(GetQuestion), new { id = question.Id }, question);
        }

        // PUT: api/Questions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(int id, [FromBody] Question question)
        {
            if (id != question.Id)
            {
                return BadRequest();
            }

            await _unitOfWork.QuestionRepository.UpdateAsync(question);
            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch
            {
                if (await _unitOfWork.QuestionRepository.GetByIdAsync(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Questions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var question = await _unitOfWork.QuestionRepository.GetByIdAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            await _unitOfWork.QuestionRepository.DeleteAsync(id);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
