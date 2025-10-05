using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TodoApp.Application.DTOs.TodoList;
using TodoApp.Application.Services;

namespace TodoApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoListController : ControllerBase
    {
        private readonly ITodoListService _todoListService;

        public TodoListController(ITodoListService todoListService)
        {
            _todoListService = todoListService;
        }

        // GET: api/todolist/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAllByUserId(Guid userId)
        {
            var lists = await _todoListService.GetAllTodoListsAsync(userId);
            return Ok(lists);
        }

        // GET: api/todolist/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var list = await _todoListService.GetByIdAsync(id);
            if (list == null)
                return NotFound("Liste bulunamadı.");

            return Ok(list);
        }

        // POST: api/todolist
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTodoListDto dto)
        {
            var id = await _todoListService.CreateTodoListAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        // PUT: api/todolist/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTodoListDto dto)
        {
            await _todoListService.UpdateTodoListAsync(id, dto);
            return NoContent();
        }

        // DELETE: api/todolist/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _todoListService.DeleteTodoListAsync(id);
            return NoContent();
        }
    }
}
