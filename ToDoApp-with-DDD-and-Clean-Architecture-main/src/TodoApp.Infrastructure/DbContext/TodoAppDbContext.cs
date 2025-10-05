using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Aggregate.UserAggregate;
using TodoApp.Domain.Aggregate.TodoListAggregate;
using Microsoft.EntityFrameworkCore.SqlServer;
using TodoApp.Domain.ValueObjects;


namespace TodoApp.Infrastructure.DbContext
{
    public class TodoAppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public TodoAppDbContext(DbContextOptions<TodoAppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Todo> Todos => Set<Todo>();
        public DbSet<TodoList> TodoLists => Set<TodoList>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ✅ Todo yapılandırması
            modelBuilder.Entity<Todo>(builder =>
            {
                builder.HasKey(t => t.Id);

                builder.Property(t => t.Title)
                       .HasConversion(
                           title => title.Value,
                           value => new Title(value))
                       .HasMaxLength(100)
                       .IsRequired();

                builder.Property(t => t.Description)
                       .HasConversion(
                           desc => desc.Value,
                           value => new Description(value))
                       .HasMaxLength(500)
                       .IsRequired();

                builder.Property(t => t.DueDate)
                       .HasConversion(
                           date => date.Value,
                           value => new DueDate(value))
                       .IsRequired();

                builder.HasOne(t => t.TodoList)
                       .WithMany(tl => tl.Todos) 
                       .HasForeignKey(t => t.TodoListId)
                       .OnDelete(DeleteBehavior.Cascade);
            });

            // ✅ TodoList yapılandırması
            modelBuilder.Entity<TodoList>(builder =>
            {
                builder.HasKey(l => l.Id);

                builder.Property(l => l.Title)
                       .HasConversion(
                           title => title.Value,
                           value => new Title(value))
                       .HasMaxLength(100)
                       .IsRequired();

                builder.Property(l => l.Description)
                       .HasConversion(
                           desc => desc.Value,
                           value => new Description(value))
                       .HasMaxLength(500)
                       .IsRequired();

                builder.HasOne(l => l.User)
                       .WithMany(u => u.TodoLists)
                       .HasForeignKey(l => l.UserId);
            });

            // ✅ User yapılandırması
            modelBuilder.Entity<User>(builder =>
            {
                builder.HasKey(u => u.Id);

                builder.Property(u => u.Username)
                       .HasConversion(
                           un => un.Value,
                           value => new Username(value))
                       .HasMaxLength(50)
                       .IsRequired();

                builder.Property(u => u.Email)
                       .HasConversion(
                           email => email.Value,
                           value => new Email(value))
                       .HasMaxLength(100)
                       .IsRequired();

                builder.Property(u => u.PasswordHash)
                          .IsRequired()
                          .HasMaxLength(255);

                

            });

            base.OnModelCreating(modelBuilder);
        }
    }
}