using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.CompilerServices;
using ZANECO.API.Application.Common.Interfaces;
using ZANECO.API.Application.SMS;
using ZANECO.API.Infrastructure.Auth;
using ZANECO.API.Infrastructure.BackgroundJobs;
using ZANECO.API.Infrastructure.Caching;
using ZANECO.API.Infrastructure.Common;
using ZANECO.API.Infrastructure.Cors;
using ZANECO.API.Infrastructure.FileStorage;
using ZANECO.API.Infrastructure.Localization;
using ZANECO.API.Infrastructure.Mailing;
using ZANECO.API.Infrastructure.Mapping;
using ZANECO.API.Infrastructure.Middleware;
using ZANECO.API.Infrastructure.Multitenancy;
using ZANECO.API.Infrastructure.Notifications;
using ZANECO.API.Infrastructure.OpenApi;
using ZANECO.API.Infrastructure.PaddleOCR;
using ZANECO.API.Infrastructure.Persistence;
using ZANECO.API.Infrastructure.Persistence.Initialization;
using ZANECO.API.Infrastructure.RateLimiting;
using ZANECO.API.Infrastructure.SecurityHeaders;
using ZANECO.API.Infrastructure.SMS;

[assembly: InternalsVisibleTo("Infrastructure.Test")]

namespace ZANECO.API.Infrastructure;

public static class Startup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var applicationAssembly = typeof(Application.Startup).GetTypeInfo().Assembly;
        MapsterSettings.Configure();
        return services
            .AddApiVersioning()
            .AddAuth(config)
            .AddBackgroundJobs(config)
            .AddCaching(config)
            .AddCorsPolicy(config)
            .AddExceptionMiddleware()
            .AddHealthCheck()
            .AddPOLocalization(config)
            .AddMailing(config)
            .AddMediatR(Assembly.GetExecutingAssembly())
            .AddMultitenancy()
            .AddNotifications(config)
            .AddOpenApiDocumentation(config)
            .AddPersistence()
            .AddRequestLogging(config)
            .AddRateLimiterService(config)
            .AddRouting(options => options.LowercaseUrls = true)
            .AddHttpClientService()
            .AddAppServices()
            .AddServices();
    }

    public static IServiceCollection AddHttpClientService(this IServiceCollection services)
    {
        services.AddHttpClient("ocr", c =>
        {
            c.BaseAddress = new Uri("https://paddleocr.i247365.net/predict/ocr_system");
            c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        })
        .AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(30)));

        return services;
    }

    private static IServiceCollection AddAppServices(this IServiceCollection services) =>
        services
            //.AddScoped<IAttendanceService, AttendanceService>()
            .AddScoped<IDateTimeFunctions, DateTimeFunctions>()
            .AddScoped<IDocumentOcrJob, DocumentOcrJob>()
            .AddScoped<ISmsService, SmsService>();

    private static IServiceCollection AddApiVersioning(this IServiceCollection services) =>
        services.AddApiVersioning(config =>
        {
            config.DefaultApiVersion = new ApiVersion(1, 0);
            config.AssumeDefaultVersionWhenUnspecified = true;
            config.ReportApiVersions = true;
        });

    private static IServiceCollection AddHealthCheck(this IServiceCollection services) =>
        services.AddHealthChecks().AddCheck<TenantHealthCheck>("Tenant").Services;

    public static async Task InitializeDatabasesAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
    {
        // Create a new scope to retrieve scoped services
        using var scope = services.CreateScope();

        await scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>()
            .InitializeDatabasesAsync(cancellationToken);
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder, IConfiguration config) =>
        builder
            .UseRequestLocalization()
            .UseStaticFiles()
            .UseSecurityHeaders(config)
            .UseFileStorage()
            .UseExceptionMiddleware()
            .UseRouting()
            .UseCorsPolicy()
            .UseAuthentication()
            .UseCurrentUser()
            .UseMultiTenancy()
            .UseAuthorization()
            .UseRateLimiter()
            .UseRequestLogging(config)
            .UseHangfireDashboard(config)
            .UseOpenApiDocumentation(config);

    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapControllers().RequireAuthorization();
        builder.MapHealthCheck();
        builder.MapNotifications();
        return builder;
    }

    private static IEndpointConventionBuilder MapHealthCheck(this IEndpointRouteBuilder endpoints) =>
        endpoints.MapHealthChecks("/health");
}