using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using ecology_dotnet.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Force Kestrel to listen on these URLs (ignores appsettings for now)
builder.WebHost.UseUrls("http://localhost:5000", "https://localhost:5001");

// PostgreSQL (your connection string from appsettings.json)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection(); // keep off while testing HTTP

app.UseAuthorization();
app.MapControllers();

// Simple test endpoint at GET /
app.MapGet("/", () => "API is running");

app.Run();
