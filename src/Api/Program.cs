using Application.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder();

builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ApplicationDbContext>(o =>
{
    o.UseMySQL();
});

var app = builder.Build();

app.UseAuthorization();
app.UseRouting();
app.MapControllers();
app.Run();

