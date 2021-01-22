using System.Net;
using System.Threading.Tasks;
using DatascopeTest.Exceptions;
using DatascopeTest.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace DatascopeTest.Middleware
{
    public class ValidationExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                var fac = (ProblemDetailsFactory)context.RequestServices.GetService(typeof(ProblemDetailsFactory));
                var ms = new ModelStateDictionary();
                ms.AddModelErrorRange(ex.Errors);
                var resp = fac.CreateValidationProblemDetails(context, ms);
                var json = JsonConvert.SerializeObject(resp);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
