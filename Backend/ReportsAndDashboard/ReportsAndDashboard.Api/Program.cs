using InsuranceApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using ReportsAndDashboard.Application.Interfaces;
using ReportsAndDashboard.Domain.Entities;
using ReportsAndDashboard.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("dbconn")
    )
);

builder.Services.AddScoped<ICustomerReportService, CustomerReportService>();
builder.Services.AddScoped<IPolicyReportService, PolicyReportService>();
builder.Services.AddScoped<IPremiumReportService, PremiumReportService>();
builder.Services.AddScoped<IClaimReportService, ClaimReportService>();
builder.Services.AddScoped<IUnderwritingReportService,UnderwritingReportService>();
builder.Services.AddScoped<IAgentReportService, AgentReportService>();
builder.Services.AddScoped<IFinancialReportService, FinancialReportService>();
builder.Services.AddScoped<IOutstandingPremiumReportService,OutstandingPremiumReportService>();
builder.Services.AddScoped<IDashboardSummaryService,DashboardSummaryService>();
builder.Services.AddScoped<IDashboardAnalyticsService, DashboardAnalyticsService>();
builder.Services.AddScoped<IDashboardChartsService,DashboardChartsService>();
builder.Services.AddScoped<IDashboardKpiService,DashboardKpiService>();
builder.Services.AddScoped<IReportScheduleService,ReportScheduleService>();
builder.Services.AddScoped<IReportExportService,ReportExportService>();
builder.Services.AddSingleton<IReportScheduleBackgroundService, ReportScheduleBackgroundService>();

builder.Services.AddHostedService(provider =>
    (ReportScheduleBackgroundService)provider.GetRequiredService<IReportScheduleBackgroundService>());
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
