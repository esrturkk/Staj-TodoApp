using System;

namespace TodoApp.WebMvc.Models
{
    public class TodoViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }          
        public DateTime? UpdatedAt { get; set; }
    }
}
