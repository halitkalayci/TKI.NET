using Business;
using Business.Abstracts;
using Business.Concretes;
using Core.Exceptions;
using Core.Extensions;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstracts;
using DataAccess.Concretes.EntityFramework;
using DataAccess.Concretes.EntityFramework.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

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

#region Services
// TODO : Read from appsettings.
// Lifetime 
// EfCarRepository => BaseDbContext
// Birbiriyle baðlantýlý baðýmlýlýklar ayný veya uyumlu life time ile
// eklenmelidir.
// Singleton => Transient 
builder.Services.AddDbContext<BaseDbContext>();
builder.Services.AddTransient<ICarRepository, EfCarRepository>();
builder.Services.AddTransient<ICarService, CarManager>();
builder.Services.AddTransient<IBrandService, BrandManager>();
builder.Services.AddTransient<IBrandRepository, EfBrandRepository>();
builder.Services.AddTransient<IAuthService, AuthManager>();
builder.Services.AddTransient<IUserRepository, EfUserRepository>();
builder.Services.AddSingleton<ITokenHelper, JwtTokenHelper>();
builder.Services.AddBusinessServices();
#endregion

#region Authentication
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidateAudience = true,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))
        };
    });
#endregion


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
app.UseAuthentication();

app.MapControllers();

app.UseAuthorization();
app.Run();


// Middleware => Geriye cevap dönme. Exception? 