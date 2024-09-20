using project_service.Entities;

namespace project_service.Interfaces
{
    public interface ITodoRepository
    {
        Task<IEnumerable<Todo>> GetAllAsync();
        Task<Todo> GetByIdAsync(int id);
        Task AddAsync(Todo item);
        Task UpdateAsync(Todo todo);
        Task DeleteAsync(int id);
    }
}
