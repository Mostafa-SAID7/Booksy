using System.Diagnostics;
using System.Text.Json;

namespace Booksy.Security
{
    /// <summary>
    /// Comprehensive error boundary to catch ALL exceptions and prevent information leakage
    /// Ensures consistent, secure error responses
    /// </summary>
    public class ErrorBoundaryMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorBoundaryMiddleware> _logger;
        private readonly IHostEnvironment _environment;

        public ErrorBoundaryMiddleware(
            RequestDelegate next,
            ILogger<ErrorBoundaryMiddleware> logger,
            IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var traceId = Activity.Current?.Id ?? context.TraceIdentifier;
            
            try
            {
                await _next(context);

                // Handle unhandled HTTP error status codes
                if (context.Response.StatusCode >= 400)
                {
                    // Only log, don't modify response (controller already handled)
                    _logger.LogWarning(
                        "HTTP {StatusCode} - Path: {Path} - TraceId: {TraceId}",
                        context.Response.StatusCode, context.Request.Path, traceId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Unhandled exception - Path: {Path} - TraceId: {TraceId}",
                    context.Request.Path, traceId);

                await HandleExceptionAsync(context, ex, traceId);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception, string traceId)
        {
            context.Response.ContentType = "application/json";
            var response = new ErrorResponse();

            switch (exception)
            {
                // Security exceptions
                case AuthorizationException authEx:
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    response = ErrorResponse.Forbidden("Access denied", traceId);
                    break;

                case Core.Exceptions.ValidationException validationEx:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    response = ErrorResponse.BadRequest(
                        "Validation failed",
                        validationEx.Errors
                            .Select(kvp => new ErrorDetail(kvp.Key, string.Join(", ", kvp.Value)))
                            .ToList(),
                        traceId);
                    break;

                case Core.Exceptions.NotFoundException notFoundEx:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    response = ErrorResponse.NotFound(notFoundEx.Message, traceId);
                    break;

                case Core.Exceptions.ConflictException conflictEx:
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    response = ErrorResponse.Conflict(conflictEx.Message, traceId);
                    break;

                case Core.Exceptions.BusinessException businessEx:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    response = ErrorResponse.BadRequest(businessEx.Message, traceId);
                    break;

                // Unhandled exceptions - generic response to prevent info leakage
                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    
                    // Only show detailed error in development
                    if (_environment.IsDevelopment())
                    {
                        response = new ErrorResponse
                        {
                            Success = false,
                            Message = "An error occurred",
                            TraceId = traceId,
                            Details = new List<ErrorDetail>
                            {
                                new ErrorDetail("Exception", exception.GetType().Name),
                                new ErrorDetail("Message", exception.Message),
                                new ErrorDetail("StackTrace", exception.StackTrace ?? "N/A")
                            }
                        };
                    }
                    else
                    {
                        // Production: generic message only
                        response = new ErrorResponse
                        {
                            Success = false,
                            Message = "An unexpected error occurred. Please contact support.",
                            TraceId = traceId
                        };
                    }
                    break;
            }

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = _environment.IsDevelopment()
            };

            return context.Response.WriteAsJsonAsync(response, options);
        }
    }

    /// <summary>
    /// Standardized error response structure
    /// </summary>
    public class ErrorResponse
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = "";
        public string TraceId { get; set; } = "";
        public List<ErrorDetail> Details { get; set; } = new();
        public int Timestamp { get; set; } = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        // Factory methods
        public static ErrorResponse BadRequest(string message, string traceId)
            => new() { Message = message, TraceId = traceId };

        public static ErrorResponse BadRequest(string message, List<ErrorDetail> details, string traceId)
            => new() { Message = message, Details = details, TraceId = traceId };

        public static ErrorResponse Unauthorized(string message, string traceId)
            => new() { Message = message, TraceId = traceId };

        public static ErrorResponse Forbidden(string message, string traceId)
            => new() { Message = message, TraceId = traceId };

        public static ErrorResponse NotFound(string message, string traceId)
            => new() { Message = message, TraceId = traceId };

        public static ErrorResponse Conflict(string message, string traceId)
            => new() { Message = message, TraceId = traceId };
    }

    /// <summary>
    /// Individual error detail
    /// </summary>
    public class ErrorDetail
    {
        public ErrorDetail(string field, string message)
        {
            Field = field;
            Message = message;
        }

        public string Field { get; set; }
        public string Message { get; set; }
    }
}
