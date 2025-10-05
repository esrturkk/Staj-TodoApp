using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.DTOs.TodoList;

namespace TodoApp.Application.Services
{
    public interface ITodoListService
    {
        Task<Guid> CreateTodoListAsync(CreateTodoListDto dto);
        Task<List<TodoListDto>> GetAllTodoListsAsync(Guid userId);
        Task<TodoListDto?> GetByIdAsync(Guid id);
        Task UpdateTodoListAsync(Guid id, UpdateTodoListDto dto);
        Task DeleteTodoListAsync(Guid id);
    }
}
