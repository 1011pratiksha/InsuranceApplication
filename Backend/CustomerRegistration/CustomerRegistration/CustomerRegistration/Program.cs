
using Microsoft.EntityFrameworkCore;
using CustomerRegistration.Application.Interfaces;
using CustomerRegistration.Application.Mapper;
using CustomerRegistration.Infrastructure.Data;
using CustomerRegistration.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("dbconn")
    )
);

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<INomineeService, NomineeService>();
builder.Services.AddScoped<ICustomerDocumentService, CustomerDocumentService>();

builder.Services.AddAutoMapper(typeof(CustomerRegistrationMapper));

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
