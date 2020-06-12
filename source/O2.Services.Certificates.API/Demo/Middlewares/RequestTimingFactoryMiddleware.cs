using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace O2.Services.Certificates.API.Demo.Middlewares
{
    public class RequestTimingFactoryMiddleware:IMiddleware
    {
        private readonly ILogger<RequestTimingAdHocMiddleware> _logger;
        private int _requestCounter;

        public RequestTimingFactoryMiddleware(ILogger<RequestTimingAdHocMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var watch = Stopwatch.StartNew();
            await next(context);
            watch.Stop();
            Interlocked.Increment(ref _requestCounter);
            _logger.LogWarning("Request {requestNumber} took {request}ms",_requestCounter, watch.ElapsedMilliseconds);
        }
    }
}