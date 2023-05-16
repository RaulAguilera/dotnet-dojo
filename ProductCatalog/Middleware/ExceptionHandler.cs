using API.Models;
using System.Net;

namespace API.Middleware
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly string _defaultErrorMessage = "An error has ocurred";

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, Application.Interfaces.ILogger<ExceptionHandler> logger)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                logger.LogError("Exception middleware", ex);

                var errorResponse = new ErrorResponse
                {
                    Message = _defaultErrorMessage,
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}
