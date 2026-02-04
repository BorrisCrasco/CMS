using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CMS.Application;

public static class CmsApplication
{
    public static IServiceCollection AddCmsServices(
        this IServiceCollection services)
    {
        var assembly = typeof(CmsApplication).Assembly;

        var serviceTypes = assembly.GetTypes()
            .Where(t =>
                t.IsClass &&
                !t.IsAbstract &&
                t.Namespace != null &&
                t.Namespace.EndsWith(".Services"));

        foreach (var impl in serviceTypes)
        {
            var interfaces = impl.GetInterfaces();

            foreach (var iface in interfaces)
            {
                services.AddScoped(iface, impl);
            }
        }

        return services;
    }
}
