using Dashboard.application.Interface;
using Dashboard.infrastructure.Config;
using Dashboard.infrastructure.Data;
using Dashboard.infrastructure.Repository;
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
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("dbconn")
    ));

builder.Services.AddHttpClient<PolicyClient>(client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration["ServiceUrls:Policy"]!
    );
});

builder.Services.AddHttpClient<PremiumScheduleClient>(client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration["ServiceUrls:PremiumSchedule"]!
    );
});

builder.Services.AddHttpClient<ClaimClient>(client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration["ServiceUrls:ClaimManagement"]!);
});

builder.Services.AddHttpClient<KycClient>(client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration["ServiceUrls:KycVerification"]!);
});

builder.Services.AddHttpClient<AgentCustomerClient>(client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration["ServiceUrls:InsuranceAgent"]!);
});

builder.Services.AddHttpClient<AgentCommissionClient>(client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration["ServiceUrls:InsuranceAgent"]!);
});

builder.Services.AddHttpClient<CommissionPaymentClient>(client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration["ServiceUrls:InsuranceAgent"]!);
});

builder.Services.AddHttpClient<AgentAdminClient>(client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration["ServiceUrls:InsuranceAgent"]!);
});

builder.Services.AddHttpClient<KycAdminClient>(client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration["ServiceUrls:KycVerification"]!);
});

builder.Services.AddHttpClient<UnderwritingAdminClient>(client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration["ServiceUrls:Underwriting"]!);
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
