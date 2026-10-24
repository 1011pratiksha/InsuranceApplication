using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PolicyServicing.Infrastructure.Data;
using PolicyServicing.Infrastructure.Services;
using PolicySevicing.Application.Interface;
using PolicySevicing.Application.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddHttpClient<PolicyIssuanceClient>(client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration["ServiceUrls:PolicyIssuance"]!);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyApps", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("dbconn")));
builder.Services.AddAutoMapper(
    typeof(DataMapping).Assembly);

builder.Services.AddScoped<IServiceRequestService, ServiceRequestService>();
builder.Services.AddScoped<IEndorsementService, EndorsementService>();

builder.Services.AddScoped<IRenewalService, RenewalService>();

builder.Services.AddScoped<ICancellationService, CancellationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("MyApps");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
