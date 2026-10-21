using Microsoft.EntityFrameworkCore;
using PolicyIssuance.Application.Mapper;
using PolicyIssuance.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("dbconn")
    )
);

builder.Services.AddScoped<PolicyIssuance.Application.Interfaces.IPolicyService, PolicyIssuance.Infrastructure.Repositories.PolicyService>();
builder.Services.AddScoped<PolicyIssuance.Application.Interfaces.IPolicyEndorsementService, PolicyIssuance.Infrastructure.Repositories.PolicyEndorsementService>();
builder.Services.AddScoped<PolicyIssuance.Application.Interfaces.IPolicyDocumentService, PolicyIssuance.Infrastructure.Repositories.PolicyDocumentService>();
builder.Services.AddAutoMapper(typeof(PolicyIssuanceMapper));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
