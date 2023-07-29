using Microsoft.Extensions.DependencyInjection;
using Polly;
using Serilog;
using System.Net.Http.Headers;

namespace ZANECO.API.Infrastructure.PaddleOCR;
internal static class Startup
{
    private static readonly ILogger _logger = Log.ForContext(typeof(Startup));

    public static IServiceCollection AddPaddleOcrService(this IServiceCollection services)
    {
        services.AddHttpClient("ocr", c =>
        {
            c.BaseAddress = new Uri("https://paddleocr.i247365.net/predict/ocr_system");
            c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        })
        .AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(30)));

        return services;
    }
}
