
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using PlotNet.Models;

namespace PlotNet;

public static class PlotNetGenerator
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
            .Select(p => p.ParameterType)
            .Where(p => p.IsInterface);

        foreach (var t in types)
            yield return t;

        yield break;
    }

    internal static GraphDTO GeneratePayload(IServiceCollection services)
    {
        var serviceData = GetServiceData(services);

        List<Node> nodes = new();
        List<Edge> edges = new();

        foreach (var service in serviceData)
        {
            var (impl, iface, lifetime) = service;

            Node interfaceNode = GetOrCreate(iface.Name);

            interfaceNode.Data["Lifetime"] = lifetime.ToString();

            nodes.Add(interfaceNode);

            if (impl == null)
                continue;

            Node implNode = GetOrCreate(impl.Name);

            implNode.Data["Lifetime"] = lifetime.ToString();
            implNode.Data["Parent"] = interfaceNode.Id;

            nodes.Add(implNode);

            var interfaces = GetInterfacesFromConstructor(service);

            foreach(var injectedInterface in interfaces)
            {
                Node injectedInterfaceNode = GetOrCreate(injectedInterface.Name);

                edges.Add(new(implNode.Id, injectedInterfaceNode.Id));
            }
            
        }

        return new GraphDTO(nodes, edges);

        Node GetOrCreate(string id) => nodes.FirstOrDefault(n => n.Id == id) ?? new(id, new());
    }

}