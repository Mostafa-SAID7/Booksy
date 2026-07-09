using MediatR;
using System.Reflection;
using System.Security.Claims;

namespace Booksy.Security
{
    /// <summary>
    /// Pipeline behavior to validate resource ownership before handler execution
    /// Centralized check for all user-scoped operations
    /// </summary>
    public class OwnershipValidationBehavior<TRequest, TResponse> 
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IOwnershipValidatable
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<OwnershipValidationBehavior<TRequest, TResponse>> _logger;

        public OwnershipValidationBehavior(
            IHttpContextAccessor httpContextAccessor,
            ILogger<OwnershipValidationBehavior<TRequest, TResponse>> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                _logger.LogWarning("HttpContext is null for ownership validation");
                throw new AuthorizationException("Request context not available");
            }

            var user = httpContext.User;
            var userId = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = user?.IsInRole("Admin") ?? false;

            // Validate ownership
            var resourceOwnerId = request.GetResourceOwnerId();
            
            if (string.IsNullOrEmpty(resourceOwnerId))
            {
                _logger.LogWarning("Resource owner ID not available for ownership check");
                throw new AuthorizationException("Unable to validate resource ownership");
            }

            // Admin can access any resource
            if (!isAdmin && resourceOwnerId != userId)
            {
                _logger.LogWarning(
                    "Ownership check failed: User {UserId} attempted to access resource owned by {OwnerId}",
                    userId, resourceOwnerId);
                throw new AuthorizationException("You do not have permission to access this resource");
            }

            return await next();
        }
    }

    /// <summary>
    /// Interface for requests that require ownership validation
    /// </summary>
    public interface IOwnershipValidatable
    {
        /// <summary>
        /// Get the ID of the resource owner
        /// </summary>
        string GetResourceOwnerId();
    }
}
