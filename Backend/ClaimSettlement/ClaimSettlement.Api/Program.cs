
using ClaimSettlement.Application.Interface;
using ClaimSettlement.Infrastructure.Data;
using ClaimSettlement.Infrastructure.Integrations;
using ClaimSettlement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString =builder.Configuration.GetConnectionString("dbconn")
    ?? throw new InvalidOperationException("dbconn is missing from appsettings.json.");

builder.Services.AddDbContext<AppDbContext>(options =>options.UseSqlServer(connectionString));

builder.Services.AddScoped<IClaimSettlementService,ClaimSettlementService>();

var claimManagementAddress =builder.Configuration["claimManagementAddress"]
    ?? throw new InvalidOperationException("claimManagementAddress is missing from appsettings.json.");

builder.Services.AddHttpClient<ClaimManagementClient>(client =>
{
    client.BaseAddress = new Uri(claimManagementAddress.TrimEnd('/') + "/");
    client.Timeout = TimeSpan.FromSeconds(30);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
