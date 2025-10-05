using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Aggregate.UserAggregate;
using TodoApp.Domain.ValueObjects;

//Application katmanı bu arayüzü enjekte ederek use-case’leri yürütür.



namespace TodoApp.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(Email email); // login veya kayıt kontrolü için
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(Guid id);
    }
}
