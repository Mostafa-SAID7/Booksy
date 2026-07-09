using Booksy.Common.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Booksy.Filters
{
    /// <summary>
    /// Rate limiting filter to prevent API abuse
    /// Tracks requests per IP address and enforces limits
    /// </summary>
    public class RateLimitFilter : ActionFilterAttribute
    {
        private readonly int _requestsPerMinute;
        private static readonly Dictionary<string, Queue<DateTime>> RequestTimestamps = new();
        private static readonly object LockObject = new();

        public RateLimitFilter(int requestsPerMinute = 60)
        {
            _requestsPerMinute = requestsPerMinute;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var clientIp = context.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var now = DateTime.UtcNow;

            lock (LockObject)
            {
                if (!RequestTimestamps.ContainsKey(clientIp))
                {
                    RequestTimestamps[clientIp] = new Queue<DateTime>();
                }

                var queue = RequestTimestamps[clientIp];

                // Remove timestamps older than 1 minute
                while (queue.Count > 0 && (now - queue.Peek()).TotalSeconds > 60)
                {
                    queue.Dequeue();
                }

                if (queue.Count >= _requestsPerMinute)
                {
                    context.Result = new ObjectResult(
                        Result.Fail("Rate limit exceeded. Maximum requests per minute reached."))
                    {
                        StatusCode = StatusCodes.Status429TooManyRequests
                    };

                    context.HttpContext.Response.Headers.Add("Retry-After", "60");
                    return;
                }

                queue.Enqueue(now);
            }

            base.OnActionExecuting(context);
        }
    }
}
