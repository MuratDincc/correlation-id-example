using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace CorrelationId.AspNetCore.Middleware;

public class CorrelationIdMiddleware
{
    private const string CorrelationIdHeaderKey = "X-Correlation-ID";

    private readonly RequestDelegate _next;

    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task Invoke(HttpContext httpContext)
    {
        string correlationId = Guid.NewGuid().ToString();

        if (httpContext.Request.Headers.TryGetValue(CorrelationIdHeaderKey, out StringValues correlationIds))
            correlationId = correlationIds.FirstOrDefault(k => k.Equals(CorrelationIdHeaderKey));
        else
            httpContext.Request.Headers.Add(CorrelationIdHeaderKey, correlationId);

        httpContext.Response.OnStarting(() =>
        {
            if (!httpContext.Response.Headers.TryGetValue(CorrelationIdHeaderKey, out correlationIds)) 
                httpContext.Response.Headers.Add(CorrelationIdHeaderKey, correlationId);

            return Task.CompletedTask;
        });

        await _next.Invoke(httpContext);
    }
}