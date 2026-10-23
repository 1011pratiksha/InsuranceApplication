using Microsoft.EntityFrameworkCore;
using PremiumPayment.Application.Interfaces;
using PremiumPayment.Infrastructure.Data;
using PremiumPayment.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("dbconn")));


builder.Services.AddScoped<IPremiumPaymentRepository, PremiumPaymentService>();
builder.Services.AddScoped<IPaymentReceiptRepository, PaymentReceiptService>();
builder.Services.AddScoped<IPremiumRefundRepository, PremiumRefundService>();

builder.Services.AddHttpClient();

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
