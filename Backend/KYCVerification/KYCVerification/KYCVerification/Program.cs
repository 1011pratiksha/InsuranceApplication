using KycVerification.Application.Interface;
using KycVerification.Application.Mapper;
using KycVerification.Infrastructure.Config;
using KycVerification.Infrastructure.Data;
using KycVerification.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Register controller support.
builder.Services.AddControllers();

// 2. Register Swagger services.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. Register the SQL Server database context.
// The connection string key must match "dbconn" in appsettings.json.
var connectionString = builder.Configuration.GetConnectionString("dbconn");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "Connection string 'dbconn' is missing from appsettings.json.");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// 4. Register AutoMapper using the existing KYC mapping profile.
builder.Services.AddAutoMapper(
    cfg => { },
    typeof(KycMappingProfile).Assembly);

// 5. Register the HTTP client for communicating with Customer Service.
var customerApiUrl = builder.Configuration["Services:CustomerApi"];

if (string.IsNullOrWhiteSpace(customerApiUrl) ||
    !Uri.TryCreate(customerApiUrl, UriKind.Absolute, out var customerApiUri) ||
    (customerApiUri.Scheme != Uri.UriSchemeHttp &&
     customerApiUri.Scheme != Uri.UriSchemeHttps))
{
    throw new InvalidOperationException(
        "Configure a valid HTTP or HTTPS URL for 'Services:CustomerApi' in appsettings.json.");
}

builder.Services.AddHttpClient<CustomerClient>(client =>
{
    client.BaseAddress = customerApiUri;
});

// 6. Register the KYC service implementation.
builder.Services.AddScoped<IKycService, KycService>();

var app = builder.Build();

// 7. Enable Swagger and Swagger UI in Development.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Redirect the root URL to Swagger UI.
    app.MapGet("/", () => Results.Redirect("/swagger"));
}

// 8. Redirect HTTP requests to HTTPS when HTTPS is configured.
app.UseHttpsRedirection();

// 9. Enable authorization middleware.
app.UseAuthorization();

// 10. Map controller endpoints.
app.MapControllers();

// 11. Start the application.
app.Run();
