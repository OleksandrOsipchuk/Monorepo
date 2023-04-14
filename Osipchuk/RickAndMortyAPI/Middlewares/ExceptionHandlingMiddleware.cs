using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace RickAndMortyAPI.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        public ExceptionHandlingMiddleware(RequestDelegate requestDelegate,
            ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = requestDelegate;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
           
            catch (Exception ex)
            {
               await HandleEceptionAsync(context, ex.Message, HttpStatusCode.InternalServerError);
            }
        }
        private async Task HandleEceptionAsync(HttpContext context,
            string message, HttpStatusCode statusCode)
        {
            _logger.LogError(message, statusCode);
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)statusCode;
            ErrorResponse error = new ErrorResponse(message, statusCode);
            await response.WriteAsJsonAsync(error.ToString());
        }
        
    }
}
