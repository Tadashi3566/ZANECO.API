﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using ZANECO.API.Application.SMS;

namespace ZANECO.API.Infrastructure.SMS;
internal static class Startup
{
    private static readonly ILogger _logger = Log.ForContext(typeof(Startup));

    internal static IServiceCollection AddSmsService(this IServiceCollection services, IConfiguration config)
    {
        var setting = config.GetSection(nameof(SmsSettings)).Get<SmsSettings>();
        if (setting is null) return services;

        services.AddOptions<SmsSettings>()
            .BindConfiguration($"SmsSettings:{nameof(SmsSettings)}")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services
            .AddScoped<SmsSettings>()
            .AddScoped<ISmsService, SmsService>();

        return services;
    }
}
