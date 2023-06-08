using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {

            services.AddMemoryCache();
            services.AddSingleton<ICachingService, InMemoryCacheManager>();
            services.AddTransient<LoggerServiceBase, MSSQLLogger>();
            services.AddSingleton<Stopwatch>();
            services.AddHttpContextAccessor();
        }
    }
}
