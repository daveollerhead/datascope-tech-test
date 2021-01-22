using DatascopeTest.Middleware;
using Microsoft.AspNetCore.Builder;

namespace DatascopeTest.Extensions
{
    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseValidationExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ValidationExceptionHandlerMiddleware>();
        }

        public static IApplicationBuilder UseNotFoundExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<NotFoundExceptionHandlerMiddleware>();
        }

        public static IApplicationBuilder ConfigureExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}