using Castle.DynamicProxy;
using Core.Exceptions.Types;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace Core.Aspects.Autofac.Authentication
{
    public class AuthenticationAttribute : MethodInterception
    {
        private string _roles;
        private IHttpContextAccessor _httpContextAccessor;
        public AuthenticationAttribute(string roles="")
        {
            Priority = 0;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _roles = roles;
        }

        protected override void OnBefore(IInvocation invocation)
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated == false)
                throw new AuthorizeException();

            if (!String.IsNullOrEmpty(_roles))
            {
                // Kullanıcının rollerini al, gelen roller ile aralarında en az 1 uyuşma var mı kontrol
                // et
                string[] roleArray = _roles.Split(",");
                var userRoles = _httpContextAccessor.HttpContext.User.Claims.Where(i => i.Type == ClaimTypes.Role).ToList();
                var accessGranted = userRoles != null 
                    && userRoles.Any(i => roleArray.Contains(i.Value));

                if (!accessGranted)
                    throw new AuthorizeException();
            }
        }
    }
}
