using Castle.DynamicProxy;
using Core.Exceptions.Types;
using Core.Utilities.Interceptors;
using Microsoft.AspNetCore.Http;

namespace Core.Aspects.Autofac.Authentication
{
    public class AuthenticationAttribute : MethodInterception
    {
        private IHttpContextAccessor _httpContextAccessor;
        public AuthenticationAttribute()
        {
            Priority = 0;
            _httpContextAccessor = new HttpContextAccessor();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated == false)
                throw new AuthorizeException();
        }
    }
}
