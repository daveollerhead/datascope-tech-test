using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DatascopeTest.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception)
            {
                context.Response.Clear();
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}