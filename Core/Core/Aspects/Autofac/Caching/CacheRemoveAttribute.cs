using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheRemoveAttribute : MethodInterception
    {
        private string _pattern;
        private ICachingService _cachingService;
        public CacheRemoveAttribute(string pattern)
        {
            _pattern = pattern;
            _cachingService = ServiceTool.ServiceProvider.GetService<ICachingService>();
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            var s = "ICarService.GetAll";
            _cachingService.Remove(s);
        }
    }
}
