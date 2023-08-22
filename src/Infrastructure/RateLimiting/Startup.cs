using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Threading.RateLimiting;

namespace ZANECO.API.Infrastructure.RateLimiting;

internal static class Startup
{
    private static readonly ILogger _logger = Log.ForContext(typeof(Startup));

    internal static IServiceCollection AddRateLimiterService(this IServiceCollection services, IConfiguration config)
    {
        var setting = config.GetSection(nameof(RateLimiterSettings)).Get<RateLimiterSettings>();
        if (setting?.Enable != true)
            return services;

        services.AddOptions<RateLimiterSettings>()
            //.BindConfiguration($"RateLimiterSettings:{nameof(RateLimiterSettings)}")
            .BindConfiguration(nameof(RateLimiterSettings))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddScoped<RateLimiterSettings>();

        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            options.AddPolicy("fixedByIP", httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.Connection.RemoteIpAddress.ToString(),
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = setting.AutoReplenishment,
                        PermitLimit = setting.PermitLimit,
                        QueueLimit = setting.QueueLimit,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        Window = TimeSpan.FromSeconds(setting.WindowInSecond)
                    }));

            options.AddPolicy("fixed", httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.User.Identity.Name,
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = setting.AutoReplenishment,
                        PermitLimit = setting.PermitLimit,
                        QueueLimit = setting.QueueLimit,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        Window = TimeSpan.FromSeconds(setting.WindowInSecond)
                    }));

            options.AddPolicy("sliding", httpContext =>
                RateLimitPartition.GetSlidingWindowLimiter(
                    partitionKey: httpContext.User.Identity.Name,
                    factory: _ => new SlidingWindowRateLimiterOptions
                    {
                        AutoReplenishment = setting.AutoReplenishment,
                        PermitLimit = setting.PermitLimit,
                        QueueLimit = setting.QueueLimit,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        SegmentsPerWindow = setting.SegmentsPerWindow,
                        Window = TimeSpan.FromSeconds(setting.WindowInSecond)
                    }));

            options.AddPolicy("concurrency", httpContext =>
                RateLimitPartition.GetConcurrencyLimiter(
                    partitionKey: httpContext.User.Identity.Name,
                    factory: _ => new ConcurrencyLimiterOptions
                    {
                        PermitLimit = setting.PermitLimit,
                        QueueLimit = setting.QueueLimit,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst
                    }));

            options.AddPolicy("token", httpContext =>
                RateLimitPartition.GetTokenBucketLimiter(
                    partitionKey: httpContext.User.Identity.Name,
                    factory: _ => new TokenBucketRateLimiterOptions
                    {
                        AutoReplenishment = setting.AutoReplenishment,
                        QueueLimit = setting.QueueLimit,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        ReplenishmentPeriod = TimeSpan.FromSeconds(setting.ReplenishmentPeriodInSecond),
                        TokenLimit = setting.TokenLimit,
                        TokensPerPeriod = setting.TokensPerPeriod
                    }));
        });

        return services;
    }

    internal static IApplicationBuilder UseRateLimiterService(this IApplicationBuilder app, IConfiguration config)
    {
        var setting = config.GetSection(nameof(RateLimiterSettings)).Get<RateLimiterSettings>();
        if (setting is null)
            return app;

        if (setting.Enable)
            app.UseRateLimiter();

        _logger.Information("API Rate Limiter endpoint policies Enabled: {0}", setting.Enable);

        return app;
    }
}