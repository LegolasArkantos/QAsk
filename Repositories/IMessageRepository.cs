using CrudApp.Models;

namespace CrudApp.Repositories
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> GetAllAsync() ;

        Task AddAsync(Message message);

        
    }
}
