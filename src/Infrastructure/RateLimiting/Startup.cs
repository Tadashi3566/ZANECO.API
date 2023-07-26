using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.RateLimiting;

namespace ZANECO.API.Infrastructure.RateLimiting;
internal static class Startup
{
    internal static IServiceCollection AddRateLimiterService(this IServiceCollection services, IConfiguration config)
    {
        var setting = config.GetSection(nameof(RateLimiterSettings)).Get<RateLimiterSettings>();
        if (setting == null) return services;

        services.AddOptions<RateLimiterSettings>()
            .BindConfiguration($"RateLimiterSettings:{nameof(RateLimiterSettings)}")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            options.AddPolicy("fixed", httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.User.Identity.Name,
                    //partitionKey: httpContext.Connection.RemoteIpAddress.ToString(),
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = setting.AutoReplenishment,
                        PermitLimit = setting.PermitLimit,
                        QueueLimit = setting.QueueLimit,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        Window = TimeSpan.FromSeconds(setting.WindowInSecond),
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
                        Window = TimeSpan.FromSeconds(setting.WindowInSecond),
                        SegmentsPerWindow = setting.SegmentsPerWindow
                    }));

            options.AddPolicy("concurrency", httpContext =>
                RateLimitPartition.GetConcurrencyLimiter(
                    partitionKey: httpContext.User.Identity.Name,
                    factory: _ => new ConcurrencyLimiterOptions
                    {
                        PermitLimit = setting.PermitLimit,
                        QueueLimit = setting.QueueLimit,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
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
}
