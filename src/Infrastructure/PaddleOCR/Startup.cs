using Microsoft.Extensions.DependencyInjection;
using Polly;
using System.Net.Http.Headers;
using ZANECO.API.Application.Common.Interfaces;

namespace ZANECO.API.Infrastructure.PaddleOCR;
internal static class Startup
{

    public static IServiceCollection AddPaddleOcrService(this IServiceCollection services)
    {
        services.AddHttpClient("ocr", c =>
        {
            c.BaseAddress = new Uri("https://paddleocr.i247365.net/predict/ocr_system");
            c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        })
        .AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(30)));

        services.AddScoped<IDocumentOcrJob, DocumentOcrJob>();

        return services;
    }
}
