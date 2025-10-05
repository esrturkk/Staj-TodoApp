using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Application.DTOs.TodoList;
using TodoApp.Application.Mappers;
using TodoApp.Domain.Aggregate.TodoListAggregate;
using TodoApp.Domain.Repositories;
using TodoApp.Domain.ValueObjects;

namespace TodoApp.Application.Services
{
    public class TodoListService : ITodoListService
    {
        private readonly ITodoListRepository _todoListRepository;

        public TodoListService(ITodoListRepository todoListRepository)
        {
            _todoListRepository = todoListRepository;
        }

        public async Task<Guid> CreateTodoListAsync(CreateTodoListDto dto)
        {
            var title = new Title(dto.Title);
            var description = new Description(dto.Description);
            var todoList = new TodoList(dto.UserId, title, description);
            await _todoListRepository.AddAsync(todoList);
            return todoList.Id;
        }

        public async Task<List<TodoListDto>> GetAllTodoListsAsync(Guid userId)
        {
            var lists = await _todoListRepository.GetByUserIdAsync(userId);
            return lists.Select(TodoListMapper.ToDto).ToList();
        }

        public async Task<TodoListDto?> GetByIdAsync(Guid id)
        {
            var list = await _todoListRepository.GetByIdAsync(id);
            return list is null ? null : TodoListMapper.ToDto(list);
        }

        public async Task UpdateTodoListAsync(Guid id, UpdateTodoListDto dto)
        {
            var list = await _todoListRepository.GetByIdAsync(id);
            if (list is null)
                throw new KeyNotFoundException("Todo listesi bulunamadı");

            list.Update(new Title(dto.Title), new Description(dto.Description));
            await _todoListRepository.UpdateAsync(list);
        }

        public async Task DeleteTodoListAsync(Guid id)
        {
            var list = await _todoListRepository.GetByIdAsync(id);
            if (list is null)
                throw new KeyNotFoundException("Todo listesi bulunamadı");

            await _todoListRepository.DeleteAsync(list);
        }
    }
}
