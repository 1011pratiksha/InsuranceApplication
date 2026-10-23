
using KycVerification.Application.Interface;
using KycVerification.Application.Mapper;
using KycVerification.Infrastructure.Config;
using KycVerification.Infrastructure.Data;
using KycVerification.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register controllers.
builder.Services.AddControllers();

// Register Swagger services.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the database.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("dbconn")));

// Register AutoMapper.
builder.Services.AddAutoMapper(
    cfg => { },
    typeof(KycMappingProfile).Assembly);

// Register the Customer API client.
builder.Services.AddHttpClient<CustomerClient>(client =>
{
    var customerApiUrl =
        builder.Configuration["Services:CustomerApi"];

    if (string.IsNullOrWhiteSpace(customerApiUrl))
    {
        throw new InvalidOperationException(
            "Services:CustomerApi is missing in appsettings.json.");
    }

    client.BaseAddress = new Uri(customerApiUrl);
});

// Register the KYC service.
builder.Services.AddScoped<IKycService, KycService>();

var app = builder.Build();

// Enable Swagger UI in Development.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Open Swagger UI automatically at the root URL.
    app.MapGet("/", () =>
        Results.Redirect("/swagger"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
