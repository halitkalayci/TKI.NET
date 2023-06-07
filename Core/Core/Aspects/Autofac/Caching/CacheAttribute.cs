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
    public class CacheAttribute : MethodInterception
    {
        private int _duration;
        private ICachingService _cachingService;

        public CacheAttribute(int duration=60)
        {
            _duration = duration;
            _cachingService = ServiceTool.ServiceProvider.GetService<ICachingService>();
        }
        public override void Intercept(IInvocation invocation)
        {
            // ICarService.GetAll
            // ICarService.GetById.4
            var methodName = $"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}";
            var arguments = invocation.Arguments.ToList();
            var cacheKey = $"{methodName}.{string.Join(",", arguments.Select(x => x.ToString() ?? ""))}";

            if (_cachingService.IsAdded(cacheKey))
            {
                invocation.ReturnValue = _cachingService.Get(cacheKey);
                return;
            }
            invocation.Proceed();
            _cachingService.Add(cacheKey, invocation.ReturnValue, _duration);
        }
    }
}
