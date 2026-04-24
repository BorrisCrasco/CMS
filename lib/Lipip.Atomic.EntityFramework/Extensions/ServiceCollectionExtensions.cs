using FluentValidation;
using Lipip.Atomic.EntityFramework.Behaviors.Atomics;
using Lipip.Atomic.EntityFramework.Behaviors.Validations;
using Lipip.Atomic.EntityFramework.Common.Audit;
using Lipip.Atomic.EntityFramework.Common.Authentications;
using Lipip.Atomic.EntityFramework.Common.Security;
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

        services.AddScoped<DbContext>(sp => sp.GetRequiredService<TContext>());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AtomicTransactionBehavior<,>));
        services.AddScoped<IMapper, Mapper>();
        services.AddValidatorsFromAssemblies(applicationAssemblies);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddScoped<IJwtTokenService,JwtTokenService>();
        services.AddHttpContextAccessor();
        services.AddScoped<IAuditService, AuditService>();
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(applicationAssemblies));
        return services;
    }


}
