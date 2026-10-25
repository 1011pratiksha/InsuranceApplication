
using Microsoft.EntityFrameworkCore;
using PolicyIssuance.Application.Interfaces;
using PolicyIssuance.Application.Mapper;
using PolicyIssuance.Infrastructure.Data;
using PolicyIssuance.Infrastructure.Repositories;
using PolicyIssuance.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS: allow Angular frontend
builder.Services.AddCors(options =>
{
  options.AddPolicy("AngularFrontend", policy =>
  {
    policy
        .WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();
  });
});

// Database configuration
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("dbconn")));

// Application services
builder.Services.AddScoped<IPolicyService, PolicyService>();

builder.Services.AddScoped<
    IPolicyEndorsementService, PolicyEndorsementService>();

builder.Services.AddScoped<
    IPolicyDocumentService, PolicyDocumentService>();

builder.Services.AddScoped<
    IPolicyStatusService, PolicyStatusService>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(PolicyIssuanceMapper));

// Service URLs from appsettings.json
string GetServiceUrl(string name) =>
    builder.Configuration[$"ServiceUrls:{name}"]
    ?? throw new InvalidOperationException(
        $"Missing ServiceUrls:{name} configuration.");

// Register named HTTP clients
foreach (var name in new[]
{
    "Customer",
    "Kyc",
    "Product",
    "Quote",
    "Underwriting",
    "Payment",
    "Document",
    "Notification",
    "AuditLogs"
})
{
  builder.Services.AddHttpClient(name, client =>
  {
    client.BaseAddress = new Uri(GetServiceUrl(name));
    client.Timeout = TimeSpan.FromSeconds(15);
  });
}

// Policy integration service
builder.Services.AddScoped<
    IPolicyIntegrationService,
    PolicyIntegrationService>();

var app = builder.Build();

// HTTP request pipeline
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS must run before controller endpoint mapping
app.UseCors("AngularFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
