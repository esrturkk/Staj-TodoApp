using Microsoft.AspNetCore.Mvc;
using TodoApp.WebMvc.Models;
using TodoApp.WebMvc.Services;

namespace TodoApp.WebMvc.Controllers
{
    public class TodoController : Controller
    {
        private readonly TodoApiClient _api;

        public TodoController(TodoApiClient api)
        {
            _api = api;
        }

        public async Task<IActionResult> Index(Guid todoListId)
        {
            ViewBag.TodoListId = todoListId;
            var todos = await _api.GetByTodoListIdAsync(todoListId);
            return View(todos);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTodoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var todos = await _api.GetByTodoListIdAsync(model.TodoListId);
                ViewBag.TodoListId = model.TodoListId;
                return View("Index", todos);
            }

            await _api.CreateAsync(model);
            return RedirectToAction("Index", new { todoListId = model.TodoListId });
        }

        [HttpGet]
        public IActionResult Create(Guid todoListId)
        {
            var vm = new CreateTodoViewModel { TodoListId = todoListId };
            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> Update(UpdateTodoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var todos = await _api.GetByTodoListIdAsync(model.TodoListId);
                ViewBag.TodoListId = model.TodoListId;
                return View("Index", todos);
            }

            await _api.UpdateAsync(model.Id, model);
            return RedirectToAction("Index", new { todoListId = model.TodoListId });
        }

        // GET: /Todo/Edit/{id}?todoListId=...
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id, Guid todoListId)
        {
            // Tekil endpoint olmadığından: listeyi çek, id'ye göre seç
            var todos = await _api.GetByTodoListIdAsync(todoListId);
            var dto = todos.FirstOrDefault(t => t.Id == id);
            if (dto == null) return NotFound();

            var vm = new UpdateTodoViewModel
            {
                Id = dto.Id,
                TodoListId = todoListId,
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate
            };

            return View("Edit", vm); // Views/Todo/Edit.cshtml
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id, Guid todoListId)
        {
            await _api.DeleteAsync(id);
            return RedirectToAction("Index", new { todoListId });
        }

        [HttpPost]
        public async Task<IActionResult> MarkCompleted(Guid id, Guid todoListId)
        {
            await _api.MarkCompletedAsync(id);
            return RedirectToAction("Index", new { todoListId });
        }

        [HttpPost]
        public async Task<IActionResult> UnmarkCompleted(Guid id, Guid todoListId)
        {
            await _api.UnmarkCompletedAsync(id);
            return RedirectToAction("Index", new { todoListId });
        }
    }
}
