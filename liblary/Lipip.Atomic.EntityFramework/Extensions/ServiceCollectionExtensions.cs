using Azure;
using Azure.Core;
using Lipip.Atomic.EntityFramework.Behaviors;
using Lipip.Atomic.EntityFramework.Core.Atomics;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAtomicServices<TContext>(
        this IServiceCollection services,
        IConfiguration configuration,
        string connectionStringName = "DefaultConnection",
        params Assembly[] applicationAssemblies)
        where TContext : DbContext
    {
        services.AddDbContext<TContext>(options =>
        {
            var connectionString = configuration.GetConnectionString(connectionStringName);
            options.UseSqlServer(connectionString);
        });


        services.AddScoped<AtomicDbContextProxy<TContext>>();
        services.AddScoped<IAtomicUnitOfWork, AtomicUnitOfWork<TContext>>();
        services.AddScoped<DbContext>(sp => sp.GetRequiredService<TContext>());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AtomicTransactionBehavior<,>));
        services.AddScoped<IMapper, Mapper>();


        //var assemblies = AppDomain.CurrentDomain.GetAssemblies()
        //    .Where(a =>
        //        !a.IsDynamic &&
        //        !string.IsNullOrWhiteSpace(a.FullName) &&
        //        (assemblyFilter?.Invoke(a) ?? true))
        //    .ToArray();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(applicationAssemblies));


        return services;
    }


}
