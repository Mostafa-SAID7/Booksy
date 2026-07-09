using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Booksy.Filters
{
    /// <summary>
    /// HTTP caching filter for GET requests
    /// Sets Cache-Control headers for improved performance
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class CacheFilter : ActionFilterAttribute
    {
        private readonly int _durationSeconds;

        public CacheFilter(int durationSeconds = 300)
        {
            _durationSeconds = durationSeconds;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Request.Method == HttpMethods.Get &&
                context.HttpContext.Response.StatusCode == StatusCodes.Status200OK)
            {
                var cacheControlHeader = new Microsoft.Net.Http.Headers.CacheControlHeaderValue
                {
                    Public = true,
                    MaxAge = TimeSpan.FromSeconds(_durationSeconds)
                };

                context.HttpContext.Response.Headers.CacheControl = cacheControlHeader.ToString();
            }

            base.OnActionExecuted(context);
        }
    }
}
