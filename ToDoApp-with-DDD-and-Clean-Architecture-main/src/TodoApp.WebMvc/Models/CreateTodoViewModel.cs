using System;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.WebMvc.Models
{
    public class CreateTodoViewModel
    {
        [Required]
        public Guid TodoListId { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Başlık en fazla 100 karakter olmalıdır.")]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }
    }
}
