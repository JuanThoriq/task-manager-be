using TaskManager.Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;
using MediatR;
using FluentValidation;
using FluentValidation.AspNetCore;
using TaskManager.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------------------------------------------
// 1. Konfigurasi Serilog
// ----------------------------------------------------------------
var logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File(
        path: Path.Combine("logs", $"Log-{DateTime.Now:yyyyMMdd}.txt"),
        rollingInterval: RollingInterval.Infinite
    )
    .CreateLogger();

builder.Host.UseSerilog(logger);

// ----------------------------------------------------------------
// 2. Konfigurasi Connection String
// ----------------------------------------------------------------
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("TaskManagerDB");
if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("Connection string 'TaskManagerDB' tidak ditemukan di appsettings.json");
}

// ----------------------------------------------------------------
// 3. Registrasi DbContext (menggunakan DbContext Pool untuk performa)
// ----------------------------------------------------------------
builder.Services.AddDbContextPool<TaskManagerContext>(options =>
    options.UseSqlServer(connectionString));

// ----------------------------------------------------------------
// 4. Registrasi MediatR dan FluentValidation
// ----------------------------------------------------------------
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

// ----------------------------------------------------------------
// 5. Registrasi CORS (opsional: sesuaikan jika diperlukan)
// ----------------------------------------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// ----------------------------------------------------------------
// 6. Registrasi Swagger untuk dokumentasi API (opsional)
// ----------------------------------------------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ----------------------------------------------------------------
// 7. Build dan konfigurasi pipeline HTTP
// ----------------------------------------------------------------
var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();
