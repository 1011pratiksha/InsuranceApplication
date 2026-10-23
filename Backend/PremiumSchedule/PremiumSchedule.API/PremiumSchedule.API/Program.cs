using Hangfire;
using Microsoft.EntityFrameworkCore;
using PremiumSchedule.Application.Interface;
using PremiumSchedule.Application.Mapper;
using PremiumSchedule.Infrastructure.Data;
using PremiumSchedule.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("dbconn")));

builder.Services.AddScoped<IPremiumScheduleService, PremiumScheduleService>();

builder.Services.AddAutoMapper(typeof(PremiumScheduleMapper));

builder.Services.AddHangfire(config => config
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("dbconn")));
builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHangfireDashboard();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

RecurringJob.AddOrUpdate<IPremiumScheduleService>("update-overdue-installments", s => s.UpdateOverdueInstallment(), Cron.Daily());
RecurringJob.AddOrUpdate<IPremiumScheduleService>("send-todays-reminders", s => s.SendTodaysReminder(), Cron.Daily());


app.Run();
