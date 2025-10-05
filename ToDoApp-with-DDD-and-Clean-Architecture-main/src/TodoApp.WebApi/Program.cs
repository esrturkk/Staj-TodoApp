using Microsoft.EntityFrameworkCore;
using TodoApp.Application.Services;
using TodoApp.Domain.Repositories;
using TodoApp.Infrastructure;
using TodoApp.Infrastructure.DbContext;
using TodoApp.Infrastructure.Middleware;
using TodoApp.Infrastructure.Repositories;
using TodoApp.Infrastructure.Services;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dependency Injection Tan�m�
//Dependency Injection, bir s�n�f�n ihtiya� duydu�u ba��ml�l�klar� (�rne�in servis, repository gibi) kendi i�inde olu�turmamas�, bunun yerine d��ar�dan almas�d�r.
//DI Konfig�rasyonu ve PostgreSQL Ba�lant�s�
builder.Services.AddDbContext<TodoAppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, EfCoreUserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ITodoListRepository, EfCoreTodoListRepository>();
builder.Services.AddScoped<ITodoListService, TodoListService>();

builder.Services.AddScoped<ITodoRepository, EfCoreTodoRepository>();
builder.Services.AddScoped<ITodoService, TodoService>();

builder.Services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();



var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>(); //  global hata y�neticisi burada


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
