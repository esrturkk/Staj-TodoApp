using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Aggregate.TodoListAggregate;
using TodoApp.Application.DTOs.TodoList;

namespace TodoApp.Application.Services
{
    public interface ITodoService
    {
        Task<Guid> AddAsync(CreateTodoDto dto);
        Task UpdateAsync(Guid id, UpdateTodoDto dto);
        Task DeleteAsync(Guid id);
        Task MarkAsCompletedAsync(Guid id);
        Task UnmarkAsCompletedAsync(Guid id);
        Task<List<TodoDto>> GetByTodoListIdAsync(Guid todoListId);
        Task<TodoDto?> GetByIdAsync(Guid id);
    }
}
