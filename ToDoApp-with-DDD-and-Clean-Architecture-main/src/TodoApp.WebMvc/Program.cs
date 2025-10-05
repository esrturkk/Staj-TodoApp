using TodoApp.WebMvc.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//DI konfig�rasyonu
builder.Services.AddHttpClient<TodoApiClient>();
builder.Services.AddHttpClient<TodoListApiClient>();
builder.Services.AddHttpClient<UserApiClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7121/"); // API adresine g�re d�zenle
});


builder.Services.AddSession(); // session deste�i
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession(); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
