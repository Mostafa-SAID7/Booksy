using Booksy.Common.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Booksy.Filters
{
    /// <summary>
    /// Model validation filter for consistent validation error responses
    /// Returns standardized Result wrapper with validation errors
    /// </summary>
    public class ValidateModelFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(kvp => kvp.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList());

                var errorList = errors
                    .Select(e => new Error(e.Key, string.Join(", ", e.Value)))
                    .ToList();

                context.Result = new BadRequestObjectResult(
                    Result.Fail("Model validation failed", errorList));
            }

            base.OnActionExecuting(context);
        }
    }
}
