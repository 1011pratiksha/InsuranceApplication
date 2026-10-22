using InsuranceAgent.Application.Interfaces;
using InsuranceAgent.Infrastructure.Data;
using InsuranceAgent.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(
    builder.Configuration.GetConnectionString("dbconn")
    ));


builder.Services.AddScoped<IAgentService, AgentService>();
builder.Services.AddScoped<IAgentCustomerService, AgentCustomerService>();
builder.Services.AddScoped<IAgentPolicyService, AgentPolicyService>();
builder.Services.AddScoped<ICommissionRuleService, CommissionRuleService>();
builder.Services.AddScoped<IAgentCommissionService, AgentCommissionService>();
builder.Services.AddScoped<ICommissionPaymentService, CommissionPaymentService>();



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
