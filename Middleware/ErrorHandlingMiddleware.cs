using FluentValidation;
using Newtonsoft.Json;
using System;

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
            catch(ValidationException ex)
            {
                var errorMessages = ex.Errors.Select(x => $"{x.PropertyName} : {x.ErrorMessage}");
                var message = string.Join(' ',errorMessages);
                await HandleExceptionAsync(httpContext, message, StatusCodes.Status400BadRequest);
            }
            catch(Exception ex) 
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
