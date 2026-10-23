
using Microsoft.EntityFrameworkCore;
using Reports.Application.Interface;
using Reports.Application.Mapper;
using Reports.Infrastructure.Config;
using Reports.Infrastructure.Data;
using Reports.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("dbconn")));

// AutoMapper
builder.Services.AddAutoMapper( cfg => { },typeof(ReportMappingProfile).Assembly);

// HTTP clients
builder.Services.AddHttpClient<CustomerClient>(client =>
    client.BaseAddress = new Uri(
        builder.Configuration["Services:Customer"]!));

builder.Services.AddHttpClient<PolicyClient>(client =>
    client.BaseAddress = new Uri(
        builder.Configuration["Services:Policy"]!));

builder.Services.AddHttpClient<PremiumClient>(client =>
    client.BaseAddress = new Uri(
        builder.Configuration["Services:Premium"]!));

builder.Services.AddHttpClient<ClaimsClient>(client =>
    client.BaseAddress = new Uri(
        builder.Configuration["Services:Claims"]!));

builder.Services.AddHttpClient<UnderwritingClient>(client =>
    client.BaseAddress = new Uri(
        builder.Configuration["Services:Underwriting"]!));

builder.Services.AddHttpClient<AgentClient>(client =>
    client.BaseAddress = new Uri(
        builder.Configuration["Services:Agent"]!));

builder.Services.AddHttpClient<FinanceClient>(client =>
    client.BaseAddress = new Uri(
        builder.Configuration["Services:Finance"]!));


builder.Services.AddScoped<ReportsDataClient>();

builder.Services.AddScoped<IReportDataClient, ReportsDataClient>();

builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.AddScoped<IReportScheduleService, ReportScheduleService>();


var app = builder.Build();

// HTTP pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
