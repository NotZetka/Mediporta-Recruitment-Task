using Newtonsoft.Json;

namespace Mediporta_Recruitment_Task.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(HttpRequestException ex)
            {
                await HandleExceptionAsync(httpContext, ex.Message, StatusCodes.Status503ServiceUnavailable);
            }
            catch
            {
                await HandleExceptionAsync(httpContext, "Something went wrong", StatusCodes.Status500InternalServerError);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, string message, int statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            _logger.LogError(message);

            var result = JsonConvert.SerializeObject(new { error = message});
            return context.Response.WriteAsync(result);
        }
    }
}
