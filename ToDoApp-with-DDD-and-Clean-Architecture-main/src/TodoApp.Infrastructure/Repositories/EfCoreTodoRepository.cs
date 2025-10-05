using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Aggregate.TodoListAggregate;
using TodoApp.Domain.Repositories;
using TodoApp.Infrastructure.DbContext;

namespace TodoApp.Infrastructure.Repositories
{
    public class EfCoreTodoRepository : ITodoRepository
    {
        private readonly TodoAppDbContext _context;

        public EfCoreTodoRepository(TodoAppDbContext context)
        {
            _context = context;
        }

        public async Task<Todo?> GetByIdAsync(Guid id)
        {
            return await _context.Todos.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<Todo>> GetByTodoListIdAsync(Guid todoListId)
        {
            return await _context.Todos
                .Where(t => t.TodoListId == todoListId)
                .ToListAsync();
        }

        public async Task AddAsync(Todo todo)
        {
            await _context.Todos.AddAsync(todo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Todo todo)
        {
            _context.Todos.Update(todo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Todo todo)
        {
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
        }
    }
}
