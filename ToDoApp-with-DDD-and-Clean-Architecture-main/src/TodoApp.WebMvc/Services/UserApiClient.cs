using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TodoApp.WebMvc.Models;
using static System.Net.WebRequestMethods;

namespace TodoApp.WebMvc.Services
{
    public class UserApiClient
    {
        private readonly HttpClient _httpClient;

        public UserApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }



        public async Task<UserViewModel?> LoginAsync(LoginViewModel model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/user/login", model);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"API yanıtı başarısız: {response.StatusCode} - {error}");
                }

                return await response.Content.ReadFromJsonAsync<UserViewModel>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Login işlemi sırasında hata: {ex.Message}");
            }
        }


        public async Task RegisterAsync(RegisterViewModel model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/user/register", model);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Kayıt API başarısız: {response.StatusCode} - {error}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Kayıt işlemi sırasında hata: {ex.Message}");
            }


        }



        // --- YENİLER ---

        public async Task<List<UserViewModel>> GetAllAsync()
        {
            var res = await _httpClient.GetAsync("api/User");
            if (!res.IsSuccessStatusCode)
                throw new Exception($"Kullanıcılar getirilemedi: {res.StatusCode} - {await res.Content.ReadAsStringAsync()}");

            return await res.Content.ReadFromJsonAsync<List<UserViewModel>>() ?? new List<UserViewModel>();
        }

        public async Task<UserViewModel?> GetByIdAsync(Guid id)
        {
            var res = await _httpClient.GetAsync($"api/User/{id}");
            if (res.StatusCode == System.Net.HttpStatusCode.NotFound) return null;

            if (!res.IsSuccessStatusCode)
                throw new Exception($"Kullanıcı getirilemedi: {res.StatusCode} - {await res.Content.ReadAsStringAsync()}");

            return await res.Content.ReadFromJsonAsync<UserViewModel>();
        }

        public async Task UpdateUsernameAsync(Guid id, string newUsername)
        {
            // DÜZ STRING gönderiyoruz (JSON içinde "YeniAd" olarak serileşir)
            var res = await _httpClient.PutAsJsonAsync($"api/User/{id}/username", newUsername);
            if (!res.IsSuccessStatusCode)
                throw new Exception($"Username güncellenemedi: {res.StatusCode} - {await res.Content.ReadAsStringAsync()}");
        }

        public async Task UpdateEmailAsync(Guid id, string newEmail)
        {
            // Eğer WebApi tarafı [FromBody] string newEmail ise benzer şekilde:
            var res = await _httpClient.PutAsJsonAsync($"api/User/{id}/email", newEmail);
            if (!res.IsSuccessStatusCode)
                throw new Exception($"Email güncellenemedi: {res.StatusCode} - {await res.Content.ReadAsStringAsync()}");
        }


        public async Task DeleteAsync(Guid id)
        {
            var res = await _httpClient.DeleteAsync($"api/User/{id}");
            if (!res.IsSuccessStatusCode)
                throw new Exception($"Kullanıcı silinemedi: {res.StatusCode} - {await res.Content.ReadAsStringAsync()}");
        }
    }

   
}
