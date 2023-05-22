using CorrelationId.AspNetCore.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace CorrelationId.AspNetCore;

public static class Startup
{
    public static void AddLogService(this ConfigureHostBuilder host)
    {
        host.UseSerilog((context, configuration) => {
            configuration
                .WriteTo.File("logs.txt")
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning);
        });
    }

    public static void AddAspNetCoreServices(this IServiceCollection services)
    {
     
    }

    public static void UseAspNetCoreApplication(this WebApplication app)
    {
        app.UseMiddleware<CorrelationIdMiddleware>();
    }
}
