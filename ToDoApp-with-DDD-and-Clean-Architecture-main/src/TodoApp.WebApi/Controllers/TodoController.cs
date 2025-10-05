using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using TodoApp.Application.Services;
using TodoApp.Application.DTOs.TodoList;
using TodoApp.Domain.Aggregate.TodoListAggregate;

namespace TodoApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        // GET: api/todo/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetById(Guid id)
        {
            var todo = await _todoService.GetByIdAsync(id);
            if (todo == null)
                return NotFound();

            return Ok(todo);
        }

        // GET: api/todo/list/{todoListId}
        [HttpGet("list/{todoListId}")]
        public async Task<ActionResult<List<Todo>>> GetByTodoListId(Guid todoListId)
        {
            var todos = await _todoService.GetByTodoListIdAsync(todoListId);
            return Ok(todos);
        }

        // POST: api/todo
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateTodoDto dto)
        {
            var id = await _todoService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        // PUT: api/todo/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTodoDto dto)
        {
            await _todoService.UpdateAsync(id, dto);
            return NoContent();
        }

        // DELETE: api/todo/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _todoService.DeleteAsync(id);
            return NoContent();
        }

        // POST: api/todo/{id}/complete
        [HttpPost("{id}/complete")]
        public async Task<IActionResult> MarkAsCompleted(Guid id)
        {
            await _todoService.MarkAsCompletedAsync(id);
            return NoContent();
        }

        // POST: api/todo/{id}/uncomplete
        [HttpPost("{id}/uncomplete")]
        public async Task<IActionResult> UnmarkAsCompleted(Guid id)
        {
            await _todoService.UnmarkAsCompletedAsync(id);
            return NoContent();
        }
    }
}
