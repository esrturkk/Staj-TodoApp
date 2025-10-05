using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Domain.Aggregate.TodoListAggregate;
using TodoApp.Domain.Repositories;
using TodoApp.Domain.ValueObjects;
using TodoApp.Application.DTOs.TodoList;
using TodoApp.Application.Mappers;

namespace TodoApp.Application.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;
        private readonly ITodoListRepository _todoListRepository;

        public TodoService(ITodoRepository todoRepository, ITodoListRepository todoListRepository)
        {
            _todoRepository = todoRepository;
            _todoListRepository = todoListRepository;
        }

        public async Task<Guid> AddAsync(CreateTodoDto dto)
        {
            var todoList = await _todoListRepository.GetByIdAsync(dto.TodoListId);
            if (todoList == null)
                throw new KeyNotFoundException("Todo listesi bulunamadı.");

            if (!dto.DueDate.HasValue)
                throw new ArgumentException("DueDate alanı boş olamaz.");

            var dueDateVo = new DueDate(dto.DueDate.Value);

            var todo = new Todo(
                dto.TodoListId,
                new Title(dto.Title),
                new Description(dto.Description ?? string.Empty),
                dueDateVo
            );

            await _todoRepository.AddAsync(todo);
            return todo.Id;
        }

        public async Task UpdateAsync(Guid id, UpdateTodoDto dto)
        {
            var todo = await _todoRepository.GetByIdAsync(id);
            if (todo == null)
                throw new KeyNotFoundException("Todo bulunamadı.");

            if (!dto.DueDate.HasValue)
                throw new ArgumentException("DueDate alanı boş olamaz.");

            var dueDateVo = new DueDate(dto.DueDate.Value);

            todo.Update(
                new Title(dto.Title),
                new Description(dto.Description ?? string.Empty),
                dueDateVo
            );

            await _todoRepository.UpdateAsync(todo);
        }

        public async Task DeleteAsync(Guid id)
        {
            var todo = await _todoRepository.GetByIdAsync(id);
            if (todo == null)
                throw new KeyNotFoundException("Todo bulunamadı.");

            await _todoRepository.DeleteAsync(todo);
        }

        public async Task MarkAsCompletedAsync(Guid id)
        {
            var todo = await _todoRepository.GetByIdAsync(id);
            if (todo == null)
                throw new KeyNotFoundException("Todo bulunamadı.");

            todo.MarkAsCompleted();
            await _todoRepository.UpdateAsync(todo);
        }

        public async Task UnmarkAsCompletedAsync(Guid id)
        {
            var todo = await _todoRepository.GetByIdAsync(id);
            if (todo == null)
                throw new KeyNotFoundException("Todo bulunamadı.");

            todo.UnmarkAsCompleted();
            await _todoRepository.UpdateAsync(todo);
        }

        public async Task<List<TodoDto>> GetByTodoListIdAsync(Guid todoListId)
        {
            var todos = await _todoRepository.GetByTodoListIdAsync(todoListId);
            return todos.Select(TodoMapper.ToDto).ToList();
        }

        public async Task<TodoDto?> GetByIdAsync(Guid id)
        {
            var todo = await _todoRepository.GetByIdAsync(id);
            return todo == null ? null : TodoMapper.ToDto(todo);
        }
    }

}
