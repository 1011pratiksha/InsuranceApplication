using Dashboard.application.Interface;
using Dashboard.infrastructure.Config;
using Dashboard.infrastructure.Data;
using Dashboard.infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("dbconn")
    ));

builder.Services.AddHttpClient<PolicyClient>(client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration["ServiceUrls:Policy"]!
    );
});

builder.Services.AddHttpClient<PremiumScheduleClient>(client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration["ServiceUrls:PremiumSchedule"]!
    );
});

builder.Services.AddHttpClient<ClaimClient>(client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration["ServiceUrls:ClaimManagement"]!);
});

builder.Services.AddHttpClient<KycClient>(client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration["ServiceUrls:KycVerification"]!);
});

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

app.Run();
