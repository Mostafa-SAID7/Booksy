using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Booksy.Filters
{
    /// <summary>
    /// Authorization filter for role-based access control
    /// Validates user has required role before action executes
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizationFilter : ActionFilterAttribute
    {
        private readonly string[] _requiredRoles;

        public AuthorizationFilter(params string[] requiredRoles)
        {
            _requiredRoles = requiredRoles;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.User;

            // Check if user is authenticated
            if (!user.Identity?.IsAuthenticated ?? true)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Check if user has required role
            if (_requiredRoles.Length > 0 && !_requiredRoles.Any(role => user.IsInRole(role)))
            {
                context.Result = new ForbidResult();
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
