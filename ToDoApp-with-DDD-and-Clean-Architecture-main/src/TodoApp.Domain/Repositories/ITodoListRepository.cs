using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Aggregate.TodoListAggregate;


namespace TodoApp.Domain.Repositories
{
    public interface ITodoListRepository
    {
        Task<TodoList?> GetByIdAsync(Guid id);
        Task<List<TodoList>> GetByUserIdAsync(Guid userId); // kullanıcının tüm listeleri
        Task AddAsync(TodoList todoList);
        Task UpdateAsync(TodoList todoList);
        Task DeleteAsync(TodoList todoList);

    }
}
