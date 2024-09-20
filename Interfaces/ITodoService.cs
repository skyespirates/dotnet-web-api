using project_service.Entities;

namespace project_service.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<Todo>> GetAllTodosAsync();
        Task<Todo> GetTodoByIdAsync(int id);
        Task AddTodoAsync(Todo todo);
        Task UpdateTodoAsync(Todo todo);
        Task DeleteTodoAsync(int id);
    }
}
