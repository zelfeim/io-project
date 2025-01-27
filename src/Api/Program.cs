using Application.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ApplicationDbContext>(o =>
{
    o.UseNpgsql(builder.Configuration.GetConnectionString("ApplicationDbContext"));
});
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();

app.UseAuthorization();
app.UseRouting();
app.MapControllers();
app.Run();

public partial class Program { }