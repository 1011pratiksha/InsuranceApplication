using AutoMapper;
using KycVerification.Application.Interface;
using KycVerification.Application.Mapper;
using KycVerification.Application.DTO;
using KycVerification.Infrastructure.Data;
using KycVerification.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("dbconn")
    ));

builder.Services.AddAutoMapper(cfg => { }, typeof(KycMappingProfile));

builder.Services.AddHttpClient<CustomerClient>(client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration["Services:CustomerApi"] ?? "http://localhost:5091");

});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));

    options.AddPolicy("CustomerOnly", policy =>
        policy.RequireRole("Customer"));

    options.AddPolicy("AgentOnly", policy =>
        policy.RequireRole("Agent"));

    options.AddPolicy("AdminOrAgent", policy =>
        policy.RequireRole("Admin", "Agent"));
});

builder.Services.AddScoped<ICustomerKycServices, CustomerKycServices>();
builder.Services.AddScoped<IKycDocumentServices, KycDocumentServices>();
builder.Services.AddScoped<ICkycDetailsServices, CkycDetailsServices>();
builder.Services.AddScoped<IKycVerificationLogServices, KycVerificationLogServices>();
builder.Services.AddScoped<IDocumentTypeMasterServices,DocumentTypeMasterServices>();
builder.Services.AddScoped<IKycService, KycService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
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
