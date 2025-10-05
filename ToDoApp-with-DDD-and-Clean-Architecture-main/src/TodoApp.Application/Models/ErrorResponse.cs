using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Application.Models
{
    public class ErrorResponse
    {
        public string Message { get; set; } = string.Empty;
        public string? Detail { get; set; }
    }
}
