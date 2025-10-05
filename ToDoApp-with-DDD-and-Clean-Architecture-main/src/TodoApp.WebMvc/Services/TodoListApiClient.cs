using System.Net.Http.Json;
using TodoApp.WebMvc.Models;

namespace TodoApp.WebMvc.Services
{
    public class TodoListApiClient
    {
        private readonly HttpClient _http;

        public TodoListApiClient(HttpClient http)
        {
            _http = http;
        }

        // ✅ Tüm listeyi kullanıcıya göre getir
        public async Task<List<TodoListViewModel>> GetAllAsync(Guid userId)
        {
            return await _http.GetFromJsonAsync<List<TodoListViewModel>>(
                $"https://localhost:7121/api/todolist/user/{userId}") ?? new();
        }

        // ✅ Belirli bir TodoList getir
        public async Task<TodoListViewModel?> GetByIdAsync(Guid id)
        {
            return await _http.GetFromJsonAsync<TodoListViewModel>(
                $"https://localhost:7121/api/todolist/{id}");
        }

        // ✅ Yeni liste oluştur
        public async Task<Guid> CreateAsync(CreateTodoListViewModel viewModel)
        {
            var response = await _http.PostAsJsonAsync("https://localhost:7121/api/todolist", viewModel);
            response.EnsureSuccessStatusCode();

            var location = response.Headers.Location;
            var idString = location?.ToString()?.Split('/').Last();
            return Guid.TryParse(idString, out var id) ? id : Guid.Empty;
        }

        // ✅ Liste güncelle
        public async Task UpdateAsync(Guid id, UpdateTodoListViewModel viewModel)
        {
            var response = await _http.PutAsJsonAsync($"https://localhost:7121/api/todolist/{id}", viewModel);
            response.EnsureSuccessStatusCode();
        }

        // ✅ Liste sil
        public async Task DeleteAsync(Guid id)
        {
            var response = await _http.DeleteAsync($"https://localhost:7121/api/todolist/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
