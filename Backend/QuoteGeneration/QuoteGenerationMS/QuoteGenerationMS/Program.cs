using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuoteGenerationMS.Application.Interface;
using QuoteGenerationMS.Application.Mapper;
using QuoteGenerationMS.Infrastructure.Data;
using QuoteGenerationMS.Infrastructure.Services;
using QuoteGenerationMS.Infrastructure.Repositories;
using QuoteGenerationMS.Infrastructure.Clients;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("dbconn")));

builder.Services.AddAutoMapper(typeof(MappingData));

builder.Services.AddHttpClient<IProductManagementClient, ProductManagementClient>(client =>
{
    var baseUrl = builder.Configuration["ServiceUrls:ProductManagement"];

    if (string.IsNullOrWhiteSpace(baseUrl) ||
        !Uri.TryCreate(baseUrl, UriKind.Absolute, out var uri))
    {
        throw new InvalidOperationException(
            "Product Management service URL is missing or invalid.");
    }

    client.BaseAddress = uri;
});

builder.Services.AddHttpClient<IUnderwritingClient, UnderwritingClient>(client =>
{
    var baseUrl = builder.Configuration["ServiceUrls:Underwriting"];

    if (string.IsNullOrWhiteSpace(baseUrl) ||
        !Uri.TryCreate(baseUrl, UriKind.Absolute, out var uri))
    {
        throw new InvalidOperationException(
            "Underwriting service URL is missing or invalid.");
    }

    client.BaseAddress = uri;
});

builder.Services.AddHttpClient<ICustomerRegistrationClient, CustomerRegistrationClient>(
    client =>
    {
        var baseUrl = builder.Configuration["ServiceUrls:CustomerRegistration"];

        if (string.IsNullOrWhiteSpace(baseUrl))
            throw new InvalidOperationException(
                "Customer Registration service URL is missing.");

        client.BaseAddress = new Uri(baseUrl);
    });


builder.Services.AddScoped<IQuoteService, QuoteService>();
builder.Services.AddScoped<IPremiumCalculationService,PremiumCalculationService>();
builder.Services.AddScoped<IRiskAssessmentService,RiskAssessmentService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();
builder.Services.AddScoped<IQuoteRiderService, QuoteRiderService>();
builder.Services.AddScoped<IQuoteApprovalService,QuoteApprovalService>();
builder.Services.AddScoped<IQuoteLifecycleService,QuoteLifecycleService>();
builder.Services.AddScoped<IQuoteHistoryService,QuoteHistoryService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();