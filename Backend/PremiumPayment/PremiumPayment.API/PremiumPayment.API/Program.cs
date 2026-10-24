
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PremiumPayment.Application.Interfaces;
using PremiumPayment.Infrastructure.Data;
using PremiumPayment.Infrastructure.Repositories;
using PremiumPayment.Application.Mapper;
using System.Security.Claims;
using System.Text;
using PremiumPayment.Infrastructure.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("dbconn")));

builder.Services.AddScoped<IPremiumPaymentRepository, PremiumPaymentService>();
builder.Services.AddScoped<IPaymentReceiptRepository, PaymentReceiptService>();
builder.Services.AddScoped<IPremiumRefundRepository, PremiumRefundService>();
builder.Services.AddScoped<IPremiumScheduleService, PremiumScheduleService>();

builder.Services.AddHttpClient();

builder.Services.AddHttpClient("PolicyIssuance", client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration["PolicyIssuanceApi:BaseUrl"]!);
});

builder.Services.AddAutoMapper(cfg => { }, typeof(PremiumScheduleMapper));

var jwtKey = builder.Configuration["JwtSettings:Key"]
    ?? throw new InvalidOperationException("JWT key is missing.");

var jwtIssuer = builder.Configuration["JwtSettings:Issuer"]
    ?? throw new InvalidOperationException("JWT issuer is missing.");

var jwtAudience = builder.Configuration["JwtSettings:Audience"]
    ?? throw new InvalidOperationException("JWT audience is missing.");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)),

            ClockSkew = TimeSpan.Zero
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["access_token"];
                return Task.CompletedTask;
            },

            OnTokenValidated = async context =>
            {
                var userId = context.Principal?
                    .FindFirstValue(ClaimTypes.NameIdentifier);

                var tokenVersion = context.Principal?
                    .FindFirstValue("TokenVersion");

                if (!int.TryParse(userId, out int id) ||
                    !int.TryParse(tokenVersion, out int version))
                {
                    context.Fail("Invalid token.");
                    return;
                }

                var db = context.HttpContext.RequestServices
                    .GetRequiredService<ApplicationDbContext>();

                var user = await db.AuthUsers
                .AsNoTracking()
                    .Where(u => u.UserId == id)
                    .Select(u => new
                    {
                        u.Status,
                        u.TokenVersion
                    })
                    .FirstOrDefaultAsync();

                if (user == null ||
                    user.Status != "Active" ||
                    user.TokenVersion != version)
                {
                    context.Fail(
                        "Session expired. Please log in again.");
                }
            }
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
