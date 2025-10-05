using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TodoApp.Domain.ValueObjects;
using TodoApp.Domain.Aggregate.TodoListAggregate;

namespace TodoApp.Domain.Aggregate.UserAggregate
{
    public class User
    {

        public Guid Id { get; private set; } // Primary Key

        public Username Username { get; private set; } = default!;

        public Email Email { get; private set; } = default!;


        public DateTime CreatedAt { get; private set; }

        public string PasswordHash { get; private set; } = string.Empty;




        // Navigation property - one-to-many
        public ICollection<TodoList> TodoLists { get; private set; } = new List<TodoList>();


        // EF için boş ctor
        // ---------------------
        private User() { }

        // ---------------------
        // Kurucu
        // ---------------------
        public User(Username username, Email email)
        {
            Id = Guid.NewGuid();
            Username = username;
            Email = email;
            CreatedAt = DateTime.UtcNow;
        }

        // ---------------------
        // Davranışlar
        // ---------------------

        public void ChangeUsername(Username newUsername)
        {
            Username = newUsername;
        }

        public void ChangeEmail(Email newEmail)
        {
            Email = newEmail;
        }

        public void AddTodoList(TodoList list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (TodoLists.Any(x => x.Id == list.Id))
                return; // Varsa tekrar ekleme

            TodoLists.Add(list);
        }

        // Sadece hash değerini set eder,hash üretmek Application katmanında 
        public void SetPasswordHash(string passwordHash)
        {
            PasswordHash = passwordHash;
        }
    }
}