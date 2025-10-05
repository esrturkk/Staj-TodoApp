using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Application.DTOs.User;
using TodoApp.Application.Mappers;
using TodoApp.Application.Services;
using TodoApp.Domain.Aggregate.UserAggregate;
using TodoApp.Domain.Repositories;
using TodoApp.Domain.ValueObjects;

//_userRepository yalnızca bir arayüz; EF Core implementasyonu Infrastructure katmanında. Application, veri kaynağını bilmez.

namespace TodoApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(UserMapper.ToUserDto).ToList();
        }

        public async Task<UserDto?> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user is null ? null : UserMapper.ToUserDto(user);
        }

        public async Task<UserDto?> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(new Email(email));
            if (user is null) return null;

            if (!_passwordHasher.Verify(password, user.PasswordHash))
                return null;

            return UserMapper.ToUserDto(user);
        }

        public async Task CreateAsync(CreateUserDto dto)
        {
            var user = new User(
                new Username(dto.Username),
                new Email(dto.Email)
            );

            var hash = _passwordHasher.Hash(dto.Password);
            user.SetPasswordHash(hash);

            await _userRepository.AddAsync(user);
        }

        public async Task UpdateEmailAsync(Guid id, string newEmail)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null) throw new InvalidOperationException("Kullanıcı bulunamadı.");

            user.ChangeEmail(new Email(newEmail));
            await _userRepository.UpdateAsync(user);
        }

        public async Task UpdateUsernameAsync(Guid id, string newUsername)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null) throw new InvalidOperationException("Kullanıcı bulunamadı.");

            user.ChangeUsername(new Username(newUsername));
            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}
