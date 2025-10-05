using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Aggregate.TodoListAggregate;

namespace TodoApp.Domain.Repositories
{
    public interface ITodoRepository
    {
        Task<Todo?> GetByIdAsync(Guid id);
        Task<List<Todo>> GetByTodoListIdAsync(Guid todoListId);
        Task AddAsync(Todo todo);
        Task UpdateAsync(Todo todo);
        Task DeleteAsync(Todo todo);
    }
}
