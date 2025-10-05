using System;
using System.Collections.Generic;

namespace TodoApp.WebMvc.Models
{
    public class TodoListViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<TodoViewModel> Todos { get; set; } = new();

    }
}
