using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ProjectTrackerAPI.Services;
using ProjectTrackerAPI.Data;  // Add this line

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ProjectDbContext>(option => option.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Register services in the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "https://localhost:5173") // Allow both HTTP & HTTPS
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Register controllers
builder.Services.AddControllers();

// Register other services like EmailService
builder.Services.AddSingleton<EmailService>();
builder.Services.AddScoped<EmailUsername>();
builder.Services.AddScoped<EmailPasswordLink>();

var app = builder.Build();

// Enable CORS with the policy you defined
app.UseCors("AllowLocalhost");

app.UseAuthorization();

// Middleware and routes
app.MapControllers(); // Ensure controllers are mapped correctly

app.Run();
