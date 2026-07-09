using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Booksy.Security
{
    /// <summary>
    /// Attribute to restrict endpoint access by IP address
    /// Use for sensitive admin endpoints
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class IpRestrictionAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly string[] _allowedIps;
        private readonly ILogger<IpRestrictionAttribute> _logger;

        public IpRestrictionAttribute(params string[] allowedIps)
        {
            _allowedIps = allowedIps;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            _logger = context.HttpContext.RequestServices
                .GetRequiredService<ILogger<IpRestrictionAttribute>>();

            var remoteIp = context.HttpContext.Connection.RemoteIpAddress?.ToString();

            if (string.IsNullOrEmpty(remoteIp) || !IsIpAllowed(remoteIp))
            {
                _logger.LogWarning(
                    "IP restriction violation: Request from {RemoteIp} blocked from accessing {Path}",
                    remoteIp, context.HttpContext.Request.Path);

                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                return;
            }

            _logger.LogInformation(
                "IP restriction check passed: {RemoteIp} allowed to access {Path}",
                remoteIp, context.HttpContext.Request.Path);

            await Task.CompletedTask;
        }

        private bool IsIpAllowed(string remoteIp)
        {
            return _allowedIps.Any(allowedIp =>
            {
                // Simple exact match (can be extended for CIDR ranges)
                return remoteIp == allowedIp;
            });
        }
    }
}
