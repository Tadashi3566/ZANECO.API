using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ZANECO.API.Application.Multitenancy;
using ZANECO.API.Infrastructure.Persistence;
using ZANECO.API.Shared.Authorization;
using ZANECO.API.Shared.Multitenancy;

namespace ZANECO.API.Infrastructure.Multitenancy;

internal static class Startup
{
    internal static IServiceCollection AddMultitenancy(this IServiceCollection services)
    {
        return services
            .AddDbContext<TenantDbContext>((serviceProvider, dbContextOptions) =>
            {
                // TODO: We should probably add specific dbprovider/connectionstring setting for the tenantDb with a fallback to the main databasesettings
                var dbSettings = serviceProvider.GetRequiredService<IOptions<DatabaseSettings>>().Value;
                dbContextOptions.UseDatabase(dbSettings.DBProvider, dbSettings.ConnectionString, dbSettings);
            })
            .AddMultiTenant<FSHTenantInfo>()
                .WithClaimStrategy(FSHClaims.Tenant)
                .WithHeaderStrategy(MultitenancyConstants.TenantIdName)
                .WithQueryStringStrategy(MultitenancyConstants.TenantIdName)
                .WithEFCoreStore<TenantDbContext, FSHTenantInfo>()
                .Services
            .AddScoped<ITenantService, TenantService>();
    }

    internal static IApplicationBuilder UseMultiTenancy(this IApplicationBuilder app) =>
        app.UseMultiTenant();

    private static FinbuckleMultiTenantBuilder<FSHTenantInfo> WithQueryStringStrategy(this FinbuckleMultiTenantBuilder<FSHTenantInfo> builder, string queryStringKey) =>
        builder.WithDelegateStrategy(context =>
        {
            if (context is not HttpContext httpContext)
            {
                return Task.FromResult((string?)null);
            }

            httpContext.Request.Query.TryGetValue(queryStringKey, out var tenantIdParam);

            return Task.FromResult(tenantIdParam.ToString());
        });
}