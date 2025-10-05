using Microsoft.AspNetCore.Mvc;
using TodoApp.WebMvc.Models;
using TodoApp.WebMvc.Services;

namespace TodoApp.WebMvc.Controllers
{
    public class TodoListController : Controller
    {
        private readonly TodoListApiClient _todoListApi;

        public TodoListController(TodoListApiClient todoListApi)
        {
            _todoListApi = todoListApi;
        }

        public async Task<IActionResult> Index()
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr) || !Guid.TryParse(userIdStr, out Guid userId))
                return RedirectToAction("Login", "User");

            var todoLists = await _todoListApi.GetAllAsync(userId);
            return View(todoLists);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateTodoListViewModel model)
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr) || !Guid.TryParse(userIdStr, out Guid userId))
                return RedirectToAction("Login", "User");

            if (!ModelState.IsValid)
            {
                var currentLists = await _todoListApi.GetAllAsync(userId);
                return View("Index", currentLists);
            }

            model.UserId = userId;
            await _todoListApi.CreateAsync(model);
            return RedirectToAction("Index");
        }






        // ✅ Detayları görüntüleme
        public async Task<IActionResult> Details(Guid id)
        {
            var todoList = await _todoListApi.GetByIdAsync(id);
            if (todoList == null)
                return NotFound();

            return View(todoList);
        }

        // ✅ Güncelleme sayfası
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var todoList = await _todoListApi.GetByIdAsync(id);
            if (todoList == null)
                return NotFound();

            var model = new UpdateTodoListViewModel
            {
                Title = todoList.Title,
                Description = todoList.Description
            };

            ViewBag.TodoListId = id; // Güncellemede kullanmak için
            return View(model);
        }

        // ✅ Güncelleme işlemi
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, UpdateTodoListViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _todoListApi.UpdateAsync(id, model);
            return RedirectToAction("Index");
        }

        // ✅ Silme onayı 
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var todoList = await _todoListApi.GetByIdAsync(id);
            if (todoList == null)
                return NotFound();

            return View(todoList);
        }

        // ✅ Silme işlemi
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _todoListApi.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
