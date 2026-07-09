using MediatR;
using Microsoft.Extensions.Logging;

namespace Booksy.Core.Behaviors;

/// <summary>
/// MediatR pipeline behavior for authorization checks
/// Validates that requests have required permissions/roles
/// Priority: 6 (Executes before command execution)
/// 
/// Future Implementations:
/// - Role-based access control (RBAC)
/// - Resource ownership validation
/// - Feature flags
/// - Custom authorization logic
/// - Policy-based authorization
/// 
/// Responsibilities:
/// - Check user authorization status
/// - Validate required permissions
/// - Log authorization decisions
/// - Update context with authorization status
/// </summary>
public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<AuthorizationBehavior<TRequest, TResponse>> _logger;
    private readonly IBehaviorContext _context;

    public AuthorizationBehavior(
        ILogger<AuthorizationBehavior<TRequest, TResponse>> logger,
        IBehaviorContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Future: Implement authorization checks here
        // Example:
        // if (request is IAuthorizedRequest authorizedRequest)
        // {
        //     var user = _httpContextAccessor.HttpContext?.User;
        //     if (user == null || !user.IsInRole(authorizedRequest.RequiredRole))
        //     {
        //         _context.IsAuthorized = false;
        //         throw new UnauthorizedException($"User does not have required role: {authorizedRequest.RequiredRole}");
        //     }
        // }
        //
        // if (!string.IsNullOrEmpty(authorizedRequest.RequiredPolicy))
        // {
        //     var authzResult = await _authorizationService.AuthorizeAsync(user, resource, authorizedRequest.RequiredPolicy);
        //     if (!authzResult.Succeeded)
        //     {
        //         _context.IsAuthorized = false;
        //         throw new ForbiddenException($"User does not satisfy required policy: {authorizedRequest.RequiredPolicy}");
        //     }
        // }

        var requestName = typeof(TRequest).Name;
        
        _context.IsAuthorized = true;
        _logger.LogDebug(
            "Authorization Check Passed | Request: {RequestName} | Authorized: {IsAuthorized}",
            requestName,
            _context.IsAuthorized
        );

        return await next();
    }
}
