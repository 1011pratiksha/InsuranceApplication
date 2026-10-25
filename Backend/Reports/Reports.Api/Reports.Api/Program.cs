
using Microsoft.EntityFrameworkCore;
using Reports.Application.Interface;
using Reports.Application.Mapper;
using Reports.Infrastructure.Config;
using Reports.Infrastructure.Data;
using Reports.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("dbconn")));

// AutoMapper
builder.Services.AddAutoMapper( cfg => { },typeof(ReportMappingProfile).Assembly);

// HTTP clients
builder.Services.AddHttpClient<CustomerClient>(client =>
    client.BaseAddress = new Uri(
        builder.Configuration["Services:Customer"]!));

builder.Services.AddHttpClient<PolicyClient>(client =>
    client.BaseAddress = new Uri(
        builder.Configuration["Services:Policy"]!));

builder.Services.AddHttpClient<PremiumClient>(client =>
    client.BaseAddress = new Uri(
        builder.Configuration["Services:Premium"]!));

builder.Services.AddHttpClient<ClaimsClient>(client =>
    client.BaseAddress = new Uri(
        builder.Configuration["Services:Claims"]!));

builder.Services.AddHttpClient<UnderwritingClient>(client =>
    client.BaseAddress = new Uri(
        builder.Configuration["Services:Underwriting"]!));

builder.Services.AddHttpClient<AgentClient>(client =>
    client.BaseAddress = new Uri(
        builder.Configuration["Services:Agent"]!));

builder.Services.AddHttpClient<FinanceClient>(client =>
    client.BaseAddress = new Uri(
        builder.Configuration["Services:Finance"]!));





builder.Services.AddScoped<ReportsDataClient>();

builder.Services.AddScoped<IReportDataClient, ReportsDataClient>();

builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.AddScoped<IReportScheduleService, ReportScheduleService>();


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


builder.Services.AddCors(options =>
{
    options.AddPolicy("Angular", policy =>
    {
        policy.WithOrigins("https://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});


var app = builder.Build();

// HTTP pipeline
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
