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

//Dependency Injection Tanýmý
//Dependency Injection, bir sýnýfýn ihtiyaç duyduðu baðýmlýlýklarý (örneðin servis, repository gibi) kendi içinde oluþturmamasý, bunun yerine dýþarýdan almasýdýr.
//DI Konfigürasyonu ve PostgreSQL Baðlantýsý
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

app.UseMiddleware<ExceptionMiddleware>(); //  global hata yöneticisi burada


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
