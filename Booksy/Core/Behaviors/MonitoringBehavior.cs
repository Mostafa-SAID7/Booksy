using MediatR;
using Booksy.Infrastructure.Monitoring;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Booksy.Core.Behaviors
{
    /// <summary>
    /// CQRS behavior for monitoring command and query execution
    /// Tracks performance and logs exceptions
    /// </summary>
    public class MonitoringBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IMonitoringService _monitoringService;
        private readonly ILogger<MonitoringBehavior<TRequest, TResponse>> _logger;

        public MonitoringBehavior(
            IMonitoringService monitoringService,
            ILogger<MonitoringBehavior<TRequest, TResponse>> logger)
        {
            _monitoringService = monitoringService;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestType = typeof(TRequest).Name;
            var stopwatch = Stopwatch.StartNew();

            try
            {
                _logger.LogInformation("Executing {RequestType}", requestType);

                var response = await next();

                stopwatch.Stop();
                _logger.LogInformation(
                    "Executed {RequestType} in {ElapsedMs}ms",
                    requestType, stopwatch.ElapsedMilliseconds);

                return response;
            }
            catch (Exceptions.AuthorizationException ex)
            {
                stopwatch.Stop();
                _logger.LogWarning(
                    "Authorization failed for {RequestType} after {ElapsedMs}ms: {Message}",
                    requestType, stopwatch.ElapsedMilliseconds, ex.Message);

                // Track suspicious activity on authorization failures
                await _monitoringService.TrackSuspiciousActivityAsync(
                    "UnauthorizedAccess",
                    $"Authorization denied for {requestType}");

                throw;
            }
            catch (Exceptions.NotFoundException ex)
            {
                stopwatch.Stop();
                _logger.LogWarning(
                    "Not found for {RequestType} after {ElapsedMs}ms: {Message}",
                    requestType, stopwatch.ElapsedMilliseconds, ex.Message);

                throw;
            }
            catch (Exceptions.ValidationException ex)
            {
                stopwatch.Stop();
                _logger.LogWarning(
                    "Validation failed for {RequestType} after {ElapsedMs}ms",
                    requestType, stopwatch.ElapsedMilliseconds);

                throw;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(
                    ex,
                    "Error executing {RequestType} after {ElapsedMs}ms: {ExceptionType}",
                    requestType, stopwatch.ElapsedMilliseconds, ex.GetType().Name);

                // Track exception
                await _monitoringService.TrackExceptionAsync(
                    ex.GetType().Name,
                    ex.Message,
                    ex.StackTrace);

                throw;
            }
        }
    }
}
