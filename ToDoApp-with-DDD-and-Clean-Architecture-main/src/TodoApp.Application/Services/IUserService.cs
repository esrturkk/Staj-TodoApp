using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.DTOs.User;

//Application katmanındaki kullanıcıya ilişkin use-case’leri tanımlayan sözleşme 

namespace TodoApp.Application.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();                  // Tüm kullanıcıları getir
        Task<UserDto?> GetByIdAsync(Guid id);                      // ID ile kullanıcı getir
        Task CreateAsync(CreateUserDto dto);                       // Yeni kullanıcı oluştur
        Task<UserDto?> LoginAsync(string email, string password);

        Task UpdateEmailAsync(Guid id, string newEmail);           // Kullanıcının e-posta adresini değiştir
        Task UpdateUsernameAsync(Guid id, string newUsername);     // Kullanıcının kullanıcı adını değiştir
        Task DeleteAsync(Guid id);                                 // Kullanıcı sil
    }
}
