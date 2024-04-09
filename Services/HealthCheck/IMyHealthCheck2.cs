using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace RESTwebAPI.Services
{
    public interface IMyHealthCheck2
    {
        Task WriteAsync(HttpContext httpContext, HealthReport result);

    }
}
