using System.Net.Http.Json;
using TodoApp.WebMvc.Models;


namespace TodoApp.WebMvc.Services
{
    public class TodoApiClient
    {
        private readonly HttpClient _http;

        public TodoApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<TodoViewModel>> GetByTodoListIdAsync(Guid todoListId)
        {
            return await _http.GetFromJsonAsync<List<TodoViewModel>>($"https://localhost:7121/api/todo/list/{todoListId}") ?? new();
        }

        public async Task CreateAsync(CreateTodoViewModel viewModel)
        {
            await _http.PostAsJsonAsync("https://localhost:7121/api/todo", viewModel);
        }

        public async Task UpdateAsync(Guid id, UpdateTodoViewModel viewModel)
        {
            await _http.PutAsJsonAsync($"https://localhost:7121/api/todo/{id}", viewModel);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _http.DeleteAsync($"https://localhost:7121/api/todo/{id}");
        }

        public async Task MarkCompletedAsync(Guid id)
        {
            await _http.PostAsync($"https://localhost:7121/api/todo/{id}/complete", null);
        }

        public async Task UnmarkCompletedAsync(Guid id)
        {
            await _http.PostAsync($"https://localhost:7121/api/todo/{id}/uncomplete", null);
        }
    }
}
