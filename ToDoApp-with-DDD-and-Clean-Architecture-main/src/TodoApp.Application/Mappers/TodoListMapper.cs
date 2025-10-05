using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.DTOs.TodoList;
using TodoApp.Domain.Aggregate.TodoListAggregate;

namespace TodoApp.Application.Mappers
{
    public static class TodoListMapper
    {
        public static TodoListDto ToDto(TodoList list)
        {
            return new TodoListDto
            {
                Id = list.Id,
                Title = list.Title.Value,
                Description = list.Description.Value,
                CreatedAt = list.CreatedAt,
                UpdatedAt = list.UpdatedAt,
                // Artık TodoList içinde Todos yok, bu alanı boş geçiyoruz veya null verebiliriz
                Todos = list.Todos?.Select(TodoMapper.ToDto).ToList() ?? new List<TodoDto>()
            };
        }
    }
}
