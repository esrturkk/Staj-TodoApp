using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Aggregate.TodoListAggregate;
using TodoApp.Application.DTOs.TodoList;

namespace TodoApp.Application.Mappers
{
    public static class TodoMapper
    {
        public static TodoDto ToDto(Todo todo)
        {
            return new TodoDto
            {
                Id = todo.Id,
                Title = todo.Title.Value,
                Description = todo.Description.Value,
                DueDate = todo.DueDate.Value,
                IsCompleted = todo.IsCompleted,
                CreatedAt = todo.CreatedAt,
                UpdatedAt = todo.UpdatedAt
            };
        }
    }
}
