
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using PlotNet.Models;

namespace PlotNet;

public class PlotNetGenerator
{

    public static IEnumerable<ServiceData> GetServiceData(IServiceCollection services)
    {
        return services.Select(s => new ServiceData(s.ImplementationType, s.ServiceType, s.Lifetime));
    }

    public static IEnumerable<Type> GetInterfacesFromConstructor(ServiceData service)
    {
        var (impl, @interface, lifetime) = service;

        if (impl == null)
            yield break;

        IEnumerable<ConstructorInfo>? constructorInfos = impl.GetConstructors().ToList() ?? new List<ConstructorInfo>();

        if (!constructorInfos.Any())
            yield break;

        var types = constructorInfos
            .Where(i => i.IsPublic)
            .SelectMany(i => i.GetParameters())
            .Select(p => p.GetType())
            .Where(p => p.IsInterface);

        foreach(var t in types)
            yield return t;

        yield break;
    }


}