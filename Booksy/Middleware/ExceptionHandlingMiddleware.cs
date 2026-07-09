using Booksy.Common.Results;
using Booksy.Core.Exceptions;
using System.Net;
using System.Text.Json;
using ApplicationException = Booksy.Core.Exceptions.ApplicationException;

namespace Booksy.Middleware
{
    /// <summary>
    /// Global exception handling middleware for consistent error responses
    /// Catches all unhandled exceptions and returns standardized Result wrapper
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = new Result();

            switch (exception)
            {
                case ValidationException validationEx:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    response = Result.Fail("Validation failed", 
                        validationEx.Errors.Select(e => new Error(e.Key, string.Join(", ", e.Value))).ToList());
                    break;

                case NotFoundException notFoundEx:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    response = Result.Fail(notFoundEx.Message);
                    break;

                case ConflictException conflictEx:
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    response = Result.Fail(conflictEx.Message);
                    break;

                case BusinessException businessEx:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    response = Result.Fail(businessEx.Message);
                    break;

                case ApplicationException appEx:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    response = Result.Fail(appEx.Message);
                    break;

                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    response = Result.Fail("An unexpected error occurred. Please try again later.");
                    break;
            }

            var jsonOptions = new JsonSerializerOptions 
            { 
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false
            };

            return context.Response.WriteAsJsonAsync(response, jsonOptions);
        }
    }
}
