using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Подключение локальной базы MS SQL server
// строка для подключения к базе
var connection = "Server=localhost;Database=NoteDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";
// добавляем контекст ApplicationContext в качестве сервиса в приложение
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Note}/{action=Index}");

app.Run();