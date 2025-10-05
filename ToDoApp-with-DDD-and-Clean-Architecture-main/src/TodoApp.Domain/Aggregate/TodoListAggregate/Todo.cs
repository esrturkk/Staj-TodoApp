
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.ValueObjects;


namespace TodoApp.Domain.Aggregate.TodoListAggregate
{

    public class Todo
    {
        public Guid Id { get; private set; } // Primary Key

        public Guid TodoListId { get; private set; } // Foreign Key

        [MaxLength(100)]
        public Title Title { get; private set; } = default!;

        [MaxLength(500)]
        public Description Description { get; private set; } = default!;

        public DueDate DueDate { get; private set; } = default!;


        public bool IsCompleted { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public TodoList? TodoList { get; private set; }  // opsiyonel navigation


        // ---------------------
        // EF Core için boş constructor
        // ---------------------
        private Todo() { }


        // ---------------------
        // Kurucu (Factory davranışı)
        // ---------------------
        public Todo(Guid todoListId, Title title, Description description, DueDate dueDate)
        {
            Id = Guid.NewGuid();
            TodoListId = todoListId;
            Title = title;
            Description = description;
            DueDate = dueDate;

            IsCompleted = false;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        // ---------------------
        // Davranışlar (Business Logic)
        // ---------------------

        public void MarkAsCompleted()
        {
            if (IsCompleted)
                throw new InvalidOperationException("Görev zaten tamamlandı.");

            IsCompleted = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UnmarkAsCompleted()
        {
            if (!IsCompleted)
                throw new InvalidOperationException("Görev zaten tamamlanmamış.");

            IsCompleted = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Update(Title title, Description description, DueDate dueDate)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
            UpdatedAt = DateTime.UtcNow;
        }

        public bool IsOverdue()
        {
            return DueDate.Value.Date < DateTime.UtcNow.Date && !IsCompleted;

        }

    }
}
