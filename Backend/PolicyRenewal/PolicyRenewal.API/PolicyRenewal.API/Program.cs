using Hangfire;
using Microsoft.EntityFrameworkCore;
using PolicyRenewal.Application.Config;
using PolicyRenewal.Application.Interface;
using PolicyRenewal.Application.Mapper;
using PolicyRenewal.Infrastructure.Data;
using PolicyRenewal.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("dbconn")));

builder.Services.AddScoped<IPolicyRenewalService, PolicyRenewalService>();

builder.Services.AddAutoMapper(typeof(PolicyRenewalMapper));

//builder.Services.AddHttpClient<PolicyClient>(client =>
//{
//    client.BaseAddress = new Uri(builder.Configuration["policyAddress"] ?? "http://localhost:7134");
//});

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

RecurringJob.AddOrUpdate<IPolicyRenewalService>("expire-pending-renewals", s => s.ExpirePendingRenewals(), Cron.Daily());
RecurringJob.AddOrUpdate<IPolicyRenewalService>("send-renewal-reminders", s => s.SendRenewalReminder(), Cron.Daily());

app.Run();
