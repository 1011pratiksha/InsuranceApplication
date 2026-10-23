
using Microsoft.EntityFrameworkCore;
using Support.Application.Interfaces;
using Support.Infrastructure.Data;
using Support.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("dbconn")));

// Register Support service
builder.Services.AddScoped<ISupportService, SupportService>();
builder.Services.AddScoped<ISupportCategoryService, SupportCategoryService>();
builder.Services.AddScoped<ISupportResponseService, SupportResponseService>();
builder.Services.AddScoped<ISupportAttachmentService, SupportAttachmentService>();
builder.Services.AddScoped<ISupportHistoryService, SupportHistoryService>();

var app = builder.Build();

// Configure Swagger
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
