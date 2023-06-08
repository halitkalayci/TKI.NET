using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Logging
{
    public class LoggingAttribute : MethodInterception
    {
        private readonly LoggerServiceBase _loggerService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoggingAttribute(Type loggerService)
        {
            if (loggerService.BaseType != typeof(LoggerServiceBase))
                throw new Exception("Yanlış bir logger tipi geçildi.");

            _loggerService = ServiceTool.ServiceProvider.GetService<LoggerServiceBase>();

            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            _loggerService.Info(GetLogDetail(invocation));
        }

        private string GetLogDetail(IInvocation invocation)
        {
            List<LogParameter> logParameters = new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter()
                {
                    Name = invocation.GetConcreteMethod().Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name
                });
            }
            var user = _httpContextAccessor.HttpContext.User;
            var logDetail = new LogDetail()
            {
                MethodName = invocation.Method.Name,
                Parameters = logParameters,
                Username = user.Identity.IsAuthenticated ? user.Claims.FirstOrDefault(i => i.Type == ClaimTypes.Email).Value : "Unauthorized"
            };
            return JsonConvert.SerializeObject(logDetail);
        }
    }
}
