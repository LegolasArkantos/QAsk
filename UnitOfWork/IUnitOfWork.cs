using CrudApp.Repositories;

namespace CrudApp.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IMessageRepository MessageRepository { get; }
        IQuestionRepository QuestionRepository { get; }  
        Task<int> SaveAsync();  // Save changes to the database
    }
}
