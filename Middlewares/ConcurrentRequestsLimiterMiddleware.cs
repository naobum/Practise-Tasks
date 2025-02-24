using Microsoft.Extensions.Options;
using Practise_Tasks.Settings;

namespace Practise_Tasks.Middlewares
{
    public class ConcurrentRequestsLimiterMiddleware
    {
        private static int _currentRequests;
        private readonly RequestDelegate _next;
        private readonly int _parallelLimit;

        public ConcurrentRequestsLimiterMiddleware(RequestDelegate next, IOptions<ServiceSettings> settings)
        {
            _next = next;
            _parallelLimit = settings.Value.ParallelLimit;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (Interlocked.Increment(ref _currentRequests) > _parallelLimit)
            {
                Interlocked.Decrement(ref _currentRequests);
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("Сервер сейчас занят, попробуйте позже.");
                return;
            }

            try
            {
                await _next(context);
            }
            finally
            {
                Interlocked.Decrement(ref _currentRequests);
            }
        }
    }
}
