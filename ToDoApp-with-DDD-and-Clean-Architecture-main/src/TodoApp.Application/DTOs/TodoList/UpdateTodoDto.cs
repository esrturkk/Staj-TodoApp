using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Application.DTOs.TodoList
{
    public class UpdateTodoDto
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime? DueDate { get; set; }
    }
}
