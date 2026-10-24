using InsuranceAgent.Application.Interfaces;
using InsuranceAgent.Application.Mapper;
using InsuranceAgent.Infrastructure.Data;
using InsuranceAgent.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularAgent", Agent =>
    {
        Agent.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    });
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(
    builder.Configuration.GetConnectionString("dbconn")
    ));


// JWT Authentication Configuration
var jwtKey = builder.Configuration["JwtSettings:Key"] ?? throw new InvalidOperationException("JWT Key is missing.");

var jwtIssuer = builder.Configuration["JwtSettings:Issuer"];
var jwtAudience = builder.Configuration["JwtSettings:Audience"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtIssuer,

            ValidateAudience = true,
            ValidAudience = jwtAudience,

            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };


        // Read token from HttpOnly cookie
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["access_token"];
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();




builder.Services.AddScoped<IAgentService, AgentService>();
builder.Services.AddScoped<IAgentCustomerService, AgentCustomerService>();
builder.Services.AddScoped<IAgentPolicyService, AgentPolicyService>();
builder.Services.AddScoped<ICommissionRuleService, CommissionRuleService>();
builder.Services.AddScoped<IAgentCommissionService, AgentCommissionService>();
builder.Services.AddScoped<ICommissionPaymentService, CommissionPaymentService>();


builder.Services.AddHttpClient<PolicyClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7083/");
});


builder.Services.AddHttpClient<PremiumCalculationClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7227/");
});


builder.Services.AddHttpClient<CustomerClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7083/");
});


builder.Services.AddHttpClient<UserClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7040/");
});


builder.Services.AddHttpClient<ProductClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7225/");
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AngularAgent");

app.UseAuthentication();  
app.UseAuthorization();   

app.MapControllers();

app.Run();
