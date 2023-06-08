using Business.ValidationResolvers.Brand;
using Business.ValidationResolvers.Car;
using Entities.DTOs.Brand;
using Entities.DTOs.Car;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public static class BusinessServiceRegistration
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assembly);
            //services.AddFluentValidation();
            services.AddTransient<IValidator<CarForAddDto>, AddCarDtoValidator>();
            services.AddTransient<IValidator<BrandForAddDto>, BrandForAddDtoValidator>();
            services.AddTransient<IValidator<BrandForUpdateDto>, BrandForUpdateDtoValidator>();
            return services;
        }
    }
}
