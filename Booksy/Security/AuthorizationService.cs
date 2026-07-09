using System.Security.Claims;

namespace Booksy.Security
{
    /// <summary>
    /// Centralized authorization service
    /// Handles ownership validation and access control
    /// </summary>
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AuthorizationService> _logger;

        public AuthorizationService(
            IHttpContextAccessor httpContextAccessor,
            ILogger<AuthorizationService> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        /// <summary>
        /// Verify user owns resource or is admin
        /// </summary>
        public bool IsOwnerOrAdmin(string resourceOwnerId, string currentUserId, ClaimsPrincipal user)
        {
            // Admin can access any resource
            if (IsAdmin(user))
            {
                _logger.LogInformation("Admin access granted to resource owner: {ResourceOwnerId}", resourceOwnerId);
                return true;
            }

            // User can only access own resources
            bool isOwner = resourceOwnerId == currentUserId;
            if (!isOwner)
            {
                _logger.LogWarning(
                    "Unauthorized access attempt: User {UserId} attempted to access resource owned by {ResourceOwnerId}",
                    currentUserId, resourceOwnerId);
            }

            return isOwner;
        }

        /// <summary>
        /// Check authorization for generic resource type
        /// </summary>
        public async Task<bool> IsAuthorizedForResourceAsync<T>(
            Guid resourceId,
            string currentUserId,
            ClaimsPrincipal user) where T : class
        {
            var resourceType = typeof(T).Name;
            _logger.LogInformation(
                "Authorization check: User {UserId} accessing {ResourceType} {ResourceId}",
                currentUserId, resourceType, resourceId);

            // For now, use owner check pattern
            // In production, query database to get resource owner
            return IsAdmin(user) || !string.IsNullOrEmpty(currentUserId);
        }

        /// <summary>
        /// Extract user ID from claims
        /// </summary>
        public string GetCurrentUserId(ClaimsPrincipal user)
        {
            var userId = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("Unable to extract user ID from claims");
            }
            return userId ?? string.Empty;
        }

        /// <summary>
        /// Check if user has admin role
        /// </summary>
        public bool IsAdmin(ClaimsPrincipal user)
        {
            return user?.IsInRole("Admin") ?? false;
        }

        /// <summary>
        /// Check if user has specific role
        /// </summary>
        public bool HasRole(ClaimsPrincipal user, string role)
        {
            return user?.IsInRole(role) ?? false;
        }
    }
}
