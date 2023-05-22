using CorrelationId.AspNetCore.Constants;
using Microsoft.AspNetCore.Http;

namespace CorrelationId.AspNetCore.Extensions;

public static class HttpRequestExtensions
{
    public static string GetCorrelationId(this HttpRequest request)
    {
        request.Headers.TryGetValue(HeaderConstants.CorrelationId, out var correlationId);

        return correlationId;
    }
}
