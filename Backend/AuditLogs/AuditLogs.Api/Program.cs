
using AuditLogs.Application.Interfaces;
using AuditLogs.Infrastructure.Data;
using AuditLogs.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register database
builder.Services.AddDbContext<AppDbContext>(options =>options.UseSqlServer(
        builder.Configuration.GetConnectionString("dbconn")));

// Register Audit Log Service
builder.Services.AddScoped<IAuditLogService, AuditLogService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
