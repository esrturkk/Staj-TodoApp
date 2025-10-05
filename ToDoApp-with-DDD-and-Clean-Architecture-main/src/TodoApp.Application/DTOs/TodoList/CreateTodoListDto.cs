using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Application.DTOs.TodoList
{
    public class CreateTodoListDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid UserId { get; set; } // Liste hangi kullanıcıya ait olacak
    }
}
