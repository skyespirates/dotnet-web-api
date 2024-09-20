using Microsoft.EntityFrameworkCore;
using project_service.Data;
using project_service.Entities;
using project_service.Interfaces;

namespace project_service.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;

        public TodoRepository(TodoContext context) { 
            _context = context;
        }

        public async Task<IEnumerable<Todo>> GetAllAsync()
        {
            return await _context.Todos.ToListAsync();
        }

        public async Task AddAsync(Todo todo)
        {
            await _context.Todos.AddAsync(todo);
            await _context.SaveChangesAsync();
        }

        public async Task<Todo> GetByIdAsync(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                throw new KeyNotFoundException($"Todo with ID {id} not found.");
            }
            return todo;
        }

        public async Task UpdateAsync(Todo todo)
        {
            // Check if the todo exists in the database
            var existingTodo = await _context.Todos.FindAsync(todo.Id);
            if (existingTodo == null)
            {
                throw new KeyNotFoundException($"Todo with ID {todo.Id} not found.");
            }

            // Update the fields
            existingTodo.Title = todo.Title;
            existingTodo.isCompleted = todo.isCompleted;

            // Save changes
            await _context.SaveChangesAsync();
        }

        public async Task  DeleteAsync(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                throw new KeyNotFoundException($"Todo with ID {id} not found.");
            }
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
        }
    }
}
