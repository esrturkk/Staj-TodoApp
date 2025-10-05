using System;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.WebMvc.Models
{
    public class UpdateTodoViewModel
    {
        [Required]
        public Guid Id { get; set; }
        public Guid TodoListId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }
    }
}
