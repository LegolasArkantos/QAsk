using CrudApp.Models;

namespace CrudApp.Repositories
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<Question>> GetAllAsync();    
        Task<Question> GetByIdAsync(int id);         
        Task AddAsync(Question question);             
        Task UpdateAsync(Question question);         
        Task DeleteAsync(int id);                     
    }
}
