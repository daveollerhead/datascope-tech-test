using System.Net;
using System.Threading.Tasks;
using DatascopeTest.Exceptions;
using Microsoft.AspNetCore.Http;

namespace DatascopeTest.Middleware
{
    public class NotFoundExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public NotFoundExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NoEntityExistsException)
            {
                context.Response.Clear();
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
        }
    }
}