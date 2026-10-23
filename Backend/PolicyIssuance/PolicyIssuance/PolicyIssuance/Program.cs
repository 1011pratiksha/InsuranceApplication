using Microsoft.EntityFrameworkCore;
using PolicyIssuance.Application.Interfaces;
using PolicyIssuance.Application.Mapper;
using PolicyIssuance.Infrastructure.Data;
using PolicyIssuance.Infrastructure.Repositories;
using PolicyIssuance.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("dbconn")));

builder.Services.AddScoped<IPolicyService, PolicyService>();
builder.Services.AddScoped<IPolicyEndorsementService, PolicyEndorsementService>();
builder.Services.AddScoped<IPolicyDocumentService, PolicyDocumentService>();
builder.Services.AddScoped<IPolicyStatusService, PolicyStatusService>();

builder.Services.AddAutoMapper(typeof(PolicyIssuanceMapper));

// Register HTTP clients for other modules.
string GetServiceUrl(string name) =>
    builder.Configuration[$"ServiceUrls:{name}"]
    ?? throw new InvalidOperationException(
        $"Missing ServiceUrls:{name} configuration.");

foreach (var name in new[]
{
    "Customer", "Kyc", "Product", "Quote",
    "Underwriting", "Payment", "Document", "Notification"
})
{
    builder.Services.AddHttpClient(name, client =>
    {
        client.BaseAddress = new Uri(GetServiceUrl(name));
        client.Timeout = TimeSpan.FromSeconds(15);
    });

    builder.Services.AddHttpClient("Underwriting", client =>
    {
        client.BaseAddress = new Uri(
            builder.Configuration["ServiceUrls:Underwriting"]!);
    });
}

builder.Services.AddScoped<
    IPolicyIntegrationService, PolicyIntegrationService>();

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
