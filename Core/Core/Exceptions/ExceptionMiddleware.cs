using Core.Exceptions.Types;
using Microsoft.AspNetCore.Http;

namespace Core.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Uygulamada ne zaman exception fırlatılırsa fırlatılsın.
                // Buraya gelecek.
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            context.Response.StatusCode = 400;
            context.Response.ContentType = "application/json";

            //switch (e.GetType())
            //{
            //    case typeof(BusinessException):
            //        await HandleBusinessException();
            //        break;
            //}
            // e.GetType () == typeof(BusinessException)
            if (e is BusinessException) // C# 6.0 
                await HandleBusinessException(context, e);
            else if (e is ValidationException) // typeof(e) == typeof(ValidationException)
                await HandleValidationException(context, e);
            else
                await HandleUnknownException(context, e);
        }

        /// Polymorphism
        private Task HandleValidationException(HttpContext context, Exception e)
        {
            // Casting => Dönüştürme
            //ValidationException validationException = e as ValidationException;
            ValidationException validationException = (ValidationException)e;


            return context.Response.WriteAsync(new ErrorDetails()
            {
                Message=validationException.Errors,
                StatusCode=400
            }.ToString());
        }

        private Task HandleUnknownException(HttpContext context, Exception e)
        {
            return context.Response.WriteAsync(new ErrorDetails()
            {
                Message = "Bilinmedik Hata",
                StatusCode = 400
            }.ToString());
        }
        private Task HandleBusinessException(HttpContext context, Exception e)
        {
            return context.Response.WriteAsync(new ErrorDetails()
            {
                Message = e.Message,
                StatusCode = 400
            }.ToString());
        }
    }
}
