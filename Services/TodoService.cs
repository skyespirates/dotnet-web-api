using project_service.Interfaces;
using project_service.Entities;

namespace project_service.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _repository;

        public TodoService(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Todo>> GetAllTodosAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Todo> GetTodoByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddTodoAsync(Todo todo)
        {
            await _repository.AddAsync(todo);
        }

        public async Task UpdateTodoAsync(Todo todo)
        {
            await _repository.UpdateAsync(todo);
        }

        public async Task DeleteTodoAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
