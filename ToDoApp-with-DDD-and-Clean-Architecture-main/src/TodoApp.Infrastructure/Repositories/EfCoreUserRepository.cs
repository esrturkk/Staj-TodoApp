using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Aggregate.UserAggregate;
using TodoApp.Domain.Repositories;
using TodoApp.Domain.ValueObjects;
using TodoApp.Infrastructure.DbContext;


//Domain’de tanımladığın IUserRepository arayüzünün EF Core kullanılarak yapılmış somut implementasyonu.
//Yani amacı: User entity’si ile ilgili veritabanı işlemlerini (CRUD) yürütmek.

namespace TodoApp.Infrastructure.Repositories
{
    public class EfCoreUserRepository : IUserRepository
    {
        private readonly TodoAppDbContext _context;

        //EF Core’un DbContext’i (TodoAppDbContext) dependency injection ile alınır.
        public EfCoreUserRepository(TodoAppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Users.FindAsync(id);
            if (entity != null)
            {
                _context.Users.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.TodoLists) // ✅ Sadece TodoList Include ediliyor
                .ToListAsync();
        }

        //public async Task<User?> GetByEmailAsync(Email email)
        //{
        //    return await _context.Users
        //        .AsEnumerable()
        //        .FirstOrDefaultAsync(u => u.Email == email);
        //}

        public Task<User?> GetByEmailAsync(Email email)
        {
            return Task.FromResult(_context.Users.FirstOrDefault(u => u.Email == email));
        }


        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .Include(u => u.TodoLists)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
