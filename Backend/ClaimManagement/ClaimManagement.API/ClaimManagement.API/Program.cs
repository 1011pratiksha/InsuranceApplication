using ClaimManagement.Application.Interface;
using ClaimManagement.Application.Mapper;
using ClaimManagement.Infrastructure.Config;
using ClaimManagement.Infrastructure.Data;
using ClaimManagement.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("dbconn")));

builder.Services.AddAutoMapper(typeof(ClaimMappingProfile), typeof(InvestigationMappingProfile));

builder.Services.AddScoped<IClaimAssessmentRepository, ClaimAssessmentRepository>();
builder.Services.AddScoped<IClaimDocumentRepository, ClaimDocumentRepository>();
builder.Services.AddScoped<IClaimEligibilityRepository, ClaimEligibilityRepository>();
builder.Services.AddScoped<IClaimSettlementRepository, ClaimSettlementRepository>();
builder.Services.AddScoped<IClaimRepository, ClaimRepository>();
builder.Services.AddScoped<IInvestigationRepository, InvestigationRepository>();
builder.Services.AddScoped<ISurveyRepository, SurveyRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddHttpClient<PolicyClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["PolicyServiceBaseUrl"] ?? "http://localhost:7134");
});

builder.Services.AddHttpClient<CustomerClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["CustomerServiceBaseUrl"] ?? "http://localhost:7135");
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyApp");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
