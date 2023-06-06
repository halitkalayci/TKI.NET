using Business;
using Business.Abstracts;
using Business.Concretes;
using Core.Exceptions;
using Core.Extensions;
using DataAccess.Abstracts;
using DataAccess.Concretes.EntityFramework;
using DataAccess.Concretes.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// TKI Github deneme
builder.Services.AddControllers();
//AppDomain.CurrentDomain.GetAssemblies();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// { type:IUserRepository, concreteType:EfUserRepository }
var connectionString = builder.Configuration.GetConnectionString("TKI");
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// TODO : Read from appsettings.
builder.Services.AddDbContext<BaseDbContext>();
// Lifetime 
// EfCarRepository => BaseDbContext
// Birbiriyle baðlantýlý baðýmlýlýklar ayný veya uyumlu life time ile
// eklenmelidir.
// Singleton => Transient 
builder.Services.AddTransient<ICarRepository, EfCarRepository>();
builder.Services.AddTransient<ICarService, CarManager>();
builder.Services.AddTransient<IBrandService, BrandManager>();
builder.Services.AddTransient<IBrandRepository, EfBrandRepository>();
builder.Services.AddTransient<IAuthService, AuthManager>();
builder.Services.AddTransient<IUserRepository, EfUserRepository>();
// Reflection
builder.Services.AddBusinessServices();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI();

//app.UseMiddleware<ExceptionMiddleware>();
app.AddMiddlewaresFromCore();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


// Middleware => Geriye cevap dönme. Exception? 