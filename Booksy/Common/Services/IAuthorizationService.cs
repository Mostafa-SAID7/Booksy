namespace Booksy.Security
{
    /// <summary>
    /// Service for centralized authorization checks
    /// Handles ownership validation, role-based access, resource access
    /// </summary>
    public interface IAuthorizationService
    {
        /// <summary>
        /// Verify user can access order (owner or admin)
        /// </summary>
        bool CanUserAccessOrder(string userId, string orderOwnerId);

        /// <summary>
        /// Verify user can access review (owner or admin)
        /// </summary>
        bool CanUserAccessReview(string userId, string reviewOwnerId);

        /// <summary>
        /// Verify user owns the resource (e.g., cart, order)
        /// </summary>
        /// <param name="resourceOwnerId">ID of user who owns resource</param>
        /// <param name="currentUserId">ID of current user</param>
        /// <returns>True if user owns resource or is admin</returns>
        bool IsOwnerOrAdmin(string resourceOwnerId, string currentUserId, System.Security.Claims.ClaimsPrincipal user);

        /// <summary>
        /// Verify user owns the resource or is admin
        /// </summary>
        /// <param name="resourceId">Resource identifier (order, cart, etc.)</param>
        /// <param name="resourceType">Type of resource</param>
        /// <param name="currentUserId">Current user ID</param>
        /// <returns>True if authorized</returns>
        Task<bool> IsAuthorizedForResourceAsync<T>(Guid resourceId, string currentUserId, System.Security.Claims.ClaimsPrincipal user) where T : class;

        /// <summary>
        /// Get current user ID from claims
        /// </summary>
        string GetCurrentUserId(System.Security.Claims.ClaimsPrincipal user);

        /// <summary>
        /// Check if user has admin role
        /// </summary>
        bool IsAdmin(System.Security.Claims.ClaimsPrincipal user);

        /// <summary>
        /// Check if user has specific role
        /// </summary>
        bool HasRole(System.Security.Claims.ClaimsPrincipal user, string role);
    }
}
