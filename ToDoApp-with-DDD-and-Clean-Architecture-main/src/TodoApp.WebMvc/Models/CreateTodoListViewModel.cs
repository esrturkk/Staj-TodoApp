using System;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.WebMvc.Models
{
    public class CreateTodoListViewModel
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Başlık en fazla 100 karakter olmalıdır.")]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        // Controller'da set edildiği için View'dan gelmese de olur
        public Guid UserId { get; set; }
    }
}
