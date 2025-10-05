using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Aggregate.TodoListAggregate;
using TodoApp.Domain.Repositories;
using TodoApp.Infrastructure.DbContext;

namespace TodoApp.Infrastructure.Repositories
{
    public class EfCoreTodoListRepository : ITodoListRepository
    {
        private readonly TodoAppDbContext _context;

        public EfCoreTodoListRepository(TodoAppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TodoList todoList)
        {
            await _context.TodoLists.AddAsync(todoList);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TodoList todoList)
        {
            _context.TodoLists.Remove(todoList);
            await _context.SaveChangesAsync();
        }

        public async Task<TodoList?> GetByIdAsync(Guid id)
        {
            
            return await _context.TodoLists
                .Include(tl => tl.Todos) // ← Burası da önemli
                .FirstOrDefaultAsync(tl => tl.Id == id);
        }

        public async Task<List<TodoList>> GetByUserIdAsync(Guid userId)
        {
            //return await _context.TodoLists
            //    .Where(tl => tl.UserId == userId)
            //    .Include(tl => tl.Todos) // ← Burası da önemli
            //    .ToListAsync(); // ❌ Todos Include kaldırıldı

            return await _context.TodoLists
                .Where(tl => tl.UserId == userId)
                .Include(tl => tl.Todos)   // <- GÖREVLERİ DAHİL ET
                .AsNoTracking()            // okuma için performans
                .ToListAsync();
        }

        public async Task UpdateAsync(TodoList todoList)
        {
            _context.TodoLists.Update(todoList);
            await _context.SaveChangesAsync();
        }
    }
}
