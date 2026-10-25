
using Microsoft.EntityFrameworkCore;
using Support.Application.Interfaces;
using Support.Infrastructure.Data;
using Support.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();

// Configure CORS for Angular frontend
builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowAngular", policy =>
  {
    policy
        .WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();
  });
});

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<ICustomerApiService, CustomerApiService>(
    (serviceProvider, client) =>
    {
      var configuration = serviceProvider.GetRequiredService<IConfiguration>();
      var baseUrl = configuration["CustomerService:BaseUrl"];

      if (string.IsNullOrWhiteSpace(baseUrl))
      {
        throw new InvalidOperationException(
            "CustomerService:BaseUrl is missing.");
      }

      client.BaseAddress = new Uri(baseUrl);
    });

builder.Services.AddHttpClient<IAuditLogApiService, AuditLogApiService>(
    (serviceProvider, client) =>
    {
      var configuration = serviceProvider
          .GetRequiredService<IConfiguration>();

      var baseUrl = configuration["AuditLogService:BaseUrl"];

      if (string.IsNullOrWhiteSpace(baseUrl))
      {
        throw new InvalidOperationException(
            "AuditLogService:BaseUrl is missing.");
      }

      client.BaseAddress = new Uri(baseUrl);
    });


// Database connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("dbconn")));

// Register Support services
builder.Services.AddScoped<ISupportService, SupportService>();
builder.Services.AddScoped<ISupportCategoryService, SupportCategoryService>();
builder.Services.AddScoped<ISupportResponseService, SupportResponseService>();
builder.Services.AddScoped<ISupportAttachmentService, SupportAttachmentService>();
builder.Services.AddScoped<ISupportHistoryService, SupportHistoryService>();

var app = builder.Build();

// Configure Swagger
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS
app.UseCors("AllowAngular");

app.UseAuthorization();

app.MapControllers();

app.Run();
