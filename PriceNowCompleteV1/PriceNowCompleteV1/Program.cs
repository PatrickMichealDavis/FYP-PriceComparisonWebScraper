using Microsoft.EntityFrameworkCore;
using PriceNowCompleteV1.Data;
using PriceNowCompleteV1.Interfaces;
using PriceNowCompleteV1.Repositories;
using PriceNowCompleteV1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PriceNowDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(10, 4, 28))));//could cause version problems patrick for trouble shooting 


builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IMerchantRepository, MerchantRepository>();
builder.Services.AddScoped<IPriceRepository, PriceRepository>();
builder.Services.AddScoped<ILoggingRepository, LoggingRepository>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IMerchantService, MerchantService>();
builder.Services.AddScoped<IPriceService, PriceService>();
builder.Services.AddScoped<ILoggingService, LoggingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options.WithOrigins("http://localhost:4200")
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
