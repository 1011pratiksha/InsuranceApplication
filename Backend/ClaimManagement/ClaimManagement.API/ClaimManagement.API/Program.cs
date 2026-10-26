using ClaimManagement.Application.Interface;
using ClaimManagement.Application.Mapper;
using ClaimManagement.Infrastructure.Config;
using ClaimManagement.Infrastructure.Data;
using ClaimManagement.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
    options.AddPolicy("Angular", policy =>
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

var jwtKey = builder.Configuration["JwtSettings:Key"]
    ?? throw new InvalidOperationException("JWT key is missing");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey))
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                if (!context.Request.Headers.ContainsKey("Authorization"))
                {
                    context.Token = context.Request.Cookies["access_token"];
                }

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("Angular");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
