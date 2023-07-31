using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Serilog;
using ZANECO.API.Application.Common.Persistence;
using ZANECO.API.Domain.Common.Contracts;
using ZANECO.API.Infrastructure.Common;
using ZANECO.API.Infrastructure.Persistence.ConnectionString;
using ZANECO.API.Infrastructure.Persistence.Context;
using ZANECO.API.Infrastructure.Persistence.Initialization;
using ZANECO.API.Infrastructure.Persistence.Repository;

namespace ZANECO.API.Infrastructure.Persistence;

internal static class Startup
{
    private static readonly ILogger _logger = Log.ForContext(typeof(Startup));

    internal static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddOptions<DatabaseSettings>()
            .BindConfiguration(nameof(DatabaseSettings))
            .PostConfigure(dbSettings =>
            {
                _logger.Information("Current DB Provider: {dbProvider}", dbSettings.DBProvider);
                //_logger.Information("Enable Detailed Errors: {0}", dbSettings.EnableDetailedErrors);
                //_logger.Information("Enable Sensitive Data Logging: {0}", dbSettings.EnableSensitiveDataLogging);
                //_logger.Information("Enable Console Log: {0}", dbSettings.EnableConsoleLog);
            })
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services
            .AddDbContext<ApplicationDbContext>((p, m) =>
            {
                var dbSettings = p.GetRequiredService<IOptions<DatabaseSettings>>().Value;
                m.UseDatabase(dbSettings.DBProvider, dbSettings.ConnectionString, dbSettings);
            })

            .AddTransient<IDatabaseInitializer, DatabaseInitializer>()
            .AddTransient<ApplicationDbInitializer>()
            .AddTransient<ApplicationDbSeeder>()
            .AddServices(typeof(ICustomSeeder), ServiceLifetime.Transient)
            .AddTransient<CustomSeederRunner>()

            .AddTransient<IConnectionStringSecurer, ConnectionStringSecurer>()
            .AddTransient<IConnectionStringValidator, ConnectionStringValidator>()

            .AddRepositories();
    }

    internal static DbContextOptionsBuilder UseDatabase(this DbContextOptionsBuilder builder, string dbProvider, string connectionString, DatabaseSettings dbSettings)
    {
        return dbProvider.ToLowerInvariant() switch
        {
            DbProviderKeys.MySql => builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), dbContextOptions =>
                                                dbContextOptions.MigrationsAssembly("Migrators.MySQL")
                                                                .SchemaBehavior(MySqlSchemaBehavior.Ignore)
                                                                .EnableRetryOnFailure(dbSettings.MaxRetryCount)
                                                                .CommandTimeout(dbSettings.CommandTimeout))
                                            .EnableDetailedErrors(dbSettings.EnableDetailedErrors)
                                            .EnableSensitiveDataLogging(dbSettings.EnableSensitiveDataLogging),

            DbProviderKeys.Npgsql => builder.UseNpgsql(connectionString, dbContextOptions =>
                                                dbContextOptions.MigrationsAssembly("Migrators.PostgreSQL")
                                                                .EnableRetryOnFailure(dbSettings.MaxRetryCount)
                                                                .CommandTimeout(dbSettings.CommandTimeout))
                                            .EnableDetailedErrors(dbSettings.EnableDetailedErrors)
                                            .EnableSensitiveDataLogging(dbSettings.EnableSensitiveDataLogging),

            DbProviderKeys.Oracle => builder.UseOracle(connectionString, dbContextOptions =>
                                                dbContextOptions.MigrationsAssembly("Migrators.Oracle")
                                                                .CommandTimeout(dbSettings.CommandTimeout))
                                            .EnableDetailedErrors(dbSettings.EnableDetailedErrors)
                                            .EnableSensitiveDataLogging(dbSettings.EnableSensitiveDataLogging),

            DbProviderKeys.SqlServer => builder.UseSqlServer(connectionString, dbContextOptions =>
                                                dbContextOptions.MigrationsAssembly("Migrators.MSSQL")
                                                                .CommandTimeout(dbSettings.CommandTimeout)
                                                                .EnableRetryOnFailure(dbSettings.MaxRetryCount)
                                                                .CommandTimeout(dbSettings.CommandTimeout))
                                            .EnableDetailedErrors(dbSettings.EnableDetailedErrors)
                                            .EnableSensitiveDataLogging(dbSettings.EnableSensitiveDataLogging),

            DbProviderKeys.SqLite => builder.UseSqlite(connectionString, dbContextOptions =>
                                                dbContextOptions.MigrationsAssembly("Migrators.SqLite")
                                                                .CommandTimeout(dbSettings.CommandTimeout)
                                                                .CommandTimeout(dbSettings.CommandTimeout))
                                            .EnableDetailedErrors(dbSettings.EnableDetailedErrors)
                                            .EnableSensitiveDataLogging(dbSettings.EnableSensitiveDataLogging),

            _ => throw new InvalidOperationException($"DB Provider {dbProvider} is not supported."),
        };
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        // Add Repositories
        services.AddScoped(typeof(IRepository<>), typeof(ApplicationDbRepository<>));

        foreach (var aggregateRootType in
            typeof(IAggregateRoot).Assembly.GetExportedTypes()
                .Where(t => typeof(IAggregateRoot).IsAssignableFrom(t) && t.IsClass)
                .ToList())
        {
            // Add ReadRepositories.
            services.AddScoped(typeof(IReadRepository<>).MakeGenericType(aggregateRootType), sp =>
                sp.GetRequiredService(typeof(IRepository<>).MakeGenericType(aggregateRootType)));

            // Decorate the repositories with EventAddingRepositoryDecorators and expose them as IRepositoryWithEvents.
            services.AddScoped(typeof(IRepositoryWithEvents<>).MakeGenericType(aggregateRootType), sp =>
                Activator.CreateInstance(
                    typeof(EventAddingRepositoryDecorator<>).MakeGenericType(aggregateRootType),
                    sp.GetRequiredService(typeof(IRepository<>).MakeGenericType(aggregateRootType)))
                ?? throw new InvalidOperationException($"Couldn't create EventAddingRepositoryDecorator for aggregateRootType {aggregateRootType.Name}"));
        }

        return services;
    }
}