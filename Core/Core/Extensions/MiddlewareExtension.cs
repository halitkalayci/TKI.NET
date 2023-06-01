using Core.Exceptions;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class MiddlewareExtension
    {
        public static void AddMiddlewaresFromCore(this IApplicationBuilder builder)
        {
            // Bu kodu çağırdığımız tüm webAPI'lar burdaki sistemleri
            // entegre edicek
            builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
