using Business;
using Business.Abstracts;
using Business.Concretes;
using Core.Exceptions;
using Core.Extensions;
using DataAccess.Abstracts;
using DataAccess.Concretes.EntityFramework;
using DataAccess.Concretes.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// TKI Github deneme
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BaseDbContext>();
builder.Services.AddSingleton<ICarRepository, EfCarRepository>();
builder.Services.AddSingleton<ICarService, CarManager>();
builder.Services.AddSingleton<IBrandService, BrandManager>();
builder.Services.AddSingleton<IBrandRepository, EfBrandRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<ExceptionMiddleware>();
app.AddMiddlewaresFromCore();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


// Middleware => Geriye cevap dönme. Exception? 