using System;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.WebMvc.Models
{
    public class UpdateTodoListViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
    }
}
