
using Microsoft.EntityFrameworkCore;
using UnderwritingMS.Application.Interface;
using UnderwritingMS.Application.Mapper;
using UnderwritingMS.Infrastructure.Data;
using UnderwritingMS.Infrastructure.Services;
using UnderwritingMS.Infrastructure.Clients;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
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




builder.Services.AddHttpClient<ICustomerRegistrationClient, CustomerRegistrationClient>(client =>
{
    var url = builder.Configuration["ServiceUrls:CustomerRegistration"]
        ?? throw new InvalidOperationException(
            "Customer Registration URL is missing.");

    client.BaseAddress = new Uri(url);
});

builder.Services.AddHttpClient<IProductManagementClient, ProductManagementClient>(client =>
{
    var url = builder.Configuration["ServiceUrls:ProductManagement"]
        ?? throw new InvalidOperationException(
            "Product Management URL is missing.");

    client.BaseAddress = new Uri(url);
});

// Register KYC only when the KYC client and its API contract are ready.
builder.Services.AddHttpClient<IKycClient, KycClient>(client =>
{
    var url = builder.Configuration["ServiceUrls:Kyc"]
        ?? throw new InvalidOperationException("KYC URL is missing.");

    client.BaseAddress = new Uri(url);
});

var jwtKey = builder.Configuration["JwtSettings:Key"]
    ?? throw new InvalidOperationException("JWT signing key is missing.");

var issuer = builder.Configuration["JwtSettings:Issuer"]
    ?? throw new InvalidOperationException("JWT issuer is missing.");

var audience = builder.Configuration["JwtSettings:Audience"]
    ?? throw new InvalidOperationException("JWT audience is missing.");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,

            ValidateAudience = true,
            ValidAudience = audience,

            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();


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


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AngularFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
