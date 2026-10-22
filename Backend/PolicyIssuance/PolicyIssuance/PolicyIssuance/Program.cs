using Microsoft.EntityFrameworkCore;
using PolicyIssuance.Application.Interfaces;
using PolicyIssuance.Application.Mapper;
using PolicyIssuance.Infrastructure.Data;
using PolicyIssuance.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("dbconn")));

builder.Services.AddScoped<IPolicyService, PolicyService>();
builder.Services.AddScoped<IPolicyEndorsementService, PolicyEndorsementService>();
builder.Services.AddScoped<IPolicyDocumentService, PolicyDocumentService>();
builder.Services.AddScoped<IPolicyStatusService, PolicyStatusService>();

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
