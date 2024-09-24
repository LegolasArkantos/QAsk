using CrudApp.Data;
using CrudApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudApp.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDbContext _context;

        public QuestionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Question>> GetAllAsync()
        {
            return await _context.Question.ToListAsync();
        }

        public async Task<Question> GetByIdAsync(int id)
        {
            return await _context.Question.FindAsync(id);
        }

        public async Task AddAsync(Question question)
        {
            await _context.Question.AddAsync(question);
        }

        public async Task UpdateAsync(Question question)
        {
            _context.Entry(question).State = EntityState.Modified;
        }

        public async Task DeleteAsync(int id)
        {
            
            var question = await _context.Question.FindAsync(id);
            if (question != null)
            {
                _context.Question.Remove(question);
            }
        }
    }
}
