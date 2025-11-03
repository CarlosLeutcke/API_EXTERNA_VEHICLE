using Microsoft.EntityFrameworkCore;
using VehicleApi.Data;
using Microsoft.OpenApi.Models;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

var dbFileName = "vehicles.db";
var dataFolder = Path.Combine(AppContext.BaseDirectory, "Data");
Directory.CreateDirectory(dataFolder);
var dbPath = Path.Combine(dataFolder, dbFileName);
var connectionString = $"Data Source={dbPath}";

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    ctx.Database.EnsureCreated();
    await SeedData.EnsureSeededAsync(ctx);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program { }
