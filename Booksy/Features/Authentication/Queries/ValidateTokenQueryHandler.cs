using Booksy.Core.Interfaces;

namespace Booksy.Features.Authentication.Queries;

/// <summary>
/// Handler for validating token
/// </summary>
public class ValidateTokenQueryHandler : IQueryHandler<ValidateTokenQuery, bool>
{
    public async Task<bool> Handle(
        ValidateTokenQuery request,
        CancellationToken cancellationToken)
    {
        // This is a placeholder - implement token validation based on your JWT/token strategy
        // For example, verify JWT signature, expiration, etc.
        return await Task.FromResult(!string.IsNullOrEmpty(request.Token));
    }
}
