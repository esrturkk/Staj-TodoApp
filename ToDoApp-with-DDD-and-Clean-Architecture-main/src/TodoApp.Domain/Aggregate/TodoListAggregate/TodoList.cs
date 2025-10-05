using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.ValueObjects;
using TodoApp.Domain.Aggregate.UserAggregate;


namespace TodoApp.Domain.Aggregate.TodoListAggregate
{

    public class TodoList
    {

        public Guid Id { get; private set; } // Primary Key

        public Guid UserId { get; private set; } // Foreign Key

        [MaxLength(100)]
        public Title Title { get; private set; } = default!;

        [MaxLength(500)]
        public Description Description { get; private set; } = default!;

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public ICollection<Todo> Todos { get; set; } = new List<Todo>();


        // Navigation property - many-to-one
        public User? User { get; private set; }



        // EF için boş constructor
        // ---------------------
        private TodoList() { }

        // ---------------------
        // Kurucu
        // ---------------------
        public TodoList(Guid userId, Title title, Description description)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            UserId = userId;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        // ---------------------
        // Davranışlar
        // ---------------------

        public void Update(Title title, Description description)
        {
            Title = title;
            Description = description;
            UpdatedAt = DateTime.UtcNow;
        }

    }
}
