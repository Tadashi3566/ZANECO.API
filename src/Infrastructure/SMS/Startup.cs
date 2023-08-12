using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZANECO.API.Application.SMS;

namespace ZANECO.API.Infrastructure.Sms;

internal static class Startup
{
    internal static IServiceCollection AddSmsService(this IServiceCollection services, IConfiguration config)
    {
        var setting = config.GetSection(nameof(SmsSettings)).Get<SmsSettings>();
        if (setting is null) return services;

        services.AddOptions<SmsSettings>()
            //.BindConfiguration($"SmsSettings:{nameof(SmsSettings)}")
            .BindConfiguration(nameof(SmsSettings))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services
            //.AddScoped<SmsSettings>()
            .AddScoped<ISmsService, SmsService>();

        return services;
    }
}