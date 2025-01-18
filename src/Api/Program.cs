using Application.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder();

builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ApplicationDbContext>(o =>
{
});

var app = builder.Build();

app.UseAuthorization();
app.UseRouting();
app.MapControllers();
app.Run();

