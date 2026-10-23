
using Microsoft.EntityFrameworkCore;
using UnderwritingMS.Application.Interface;
using UnderwritingMS.Application.Mapper;
using UnderwritingMS.Infrastructure.Data;
using UnderwritingMS.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingData));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("dbconn")));

// Keep your other existing service registrations here.

builder.Services.AddScoped<IUnderwritingService,UnderwritingService>();
builder.Services.AddScoped<IMedicalUnderwritingService,MedicalUnderwritingService>();
builder.Services.AddScoped<IFinancialUnderwritingService,FinancialUnderwritingService>();
builder.Services.AddScoped<IRiskScoreService, RiskScoreService>();
builder.Services.AddScoped<IPremiumLoadingService,PremiumLoadingService>();
builder.Services.AddScoped<IUnderwritingDecisionService,UnderwritingDecisionService>();
builder.Services.AddScoped<IAuditLogService, AuditLogService>();

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
