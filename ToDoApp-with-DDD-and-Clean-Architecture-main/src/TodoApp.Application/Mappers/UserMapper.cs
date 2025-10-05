using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.DTOs.User;
using TodoApp.Domain.Aggregate.UserAggregate;

//Domain Entity → DTO dönüşümünü yapan yardımcı sınıftır.
//Amaç kod tekrarini azaltmak ve kodun okunabilirliğini artırmak

namespace TodoApp.Application.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToUserDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Username = user.Username.Value,
                Email = user.Email.Value,
                CreatedAt = user.CreatedAt
            };
        }
    }
}
