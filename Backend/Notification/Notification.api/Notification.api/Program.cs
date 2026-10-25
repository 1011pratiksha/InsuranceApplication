using Hangfire;
using Microsoft.EntityFrameworkCore;
using Notification.application.Interface;
using Notification.application.Mapper;
using Notification.infrastructure.Data;
using Notification.infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database connection
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(
builder.Configuration.GetConnectionString("dbconn")
));

// Register Repository
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

// Register AutoMapper
builder.Services.AddAutoMapper(cfg => { }, typeof(NotificationMappingProfile));
builder.Services.AddHangfire(configuration =>
    configuration.UseSqlServerStorage("dbconn"));
builder.Services.AddHangfireServer();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard();

app.Run();
