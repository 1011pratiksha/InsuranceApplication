using Microsoft.EntityFrameworkCore;
using ProductManagementMS.Infrastructure.Data;
using ProductManagementMS.Application.Interface;
using ProductManagementMS.Application.Mapper;
using ProductManagementMS.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register SQL Server DbContext
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("dbconn")));

builder.Services.AddAutoMapper(typeof(MappingData));

builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<IInsuranceProductService,InsuranceProductService>();
builder.Services.AddScoped<ICoverageService, CoverageService>();
builder.Services.AddScoped<IRiderService, RiderService>();
builder.Services.AddScoped<IBenefitService, BenefitService>();
builder.Services.AddScoped<IPremiumFrequencyService,PremiumFrequencyService>();
builder.Services.AddScoped<IProductCommissionService,ProductCommissionService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
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

app.UseCors("AngularFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();

