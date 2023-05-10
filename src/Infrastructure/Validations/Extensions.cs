using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ZANECO.API.Infrastructure.Validations;

public static class Extensions
{
    public static IServiceCollection AddBehaviours(this IServiceCollection services, Assembly assemblyContainingValidators)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }
}