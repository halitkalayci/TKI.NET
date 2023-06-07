using Castle.DynamicProxy;
using Core.Exceptions.Types;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Authentication
{
    public class AuthenticationAttribute : MethodInterception
    {
        private IHttpContextAccessor _httpContextAccessor;
        public AuthenticationAttribute()
        {
            Priority = 0;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated == false)
                throw new AuthorizeException();
        }
    }
}
