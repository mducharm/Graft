using Microsoft.Extensions.DependencyInjection;

namespace PlotNet.Models;

public record ServiceData(
    Type? Implementation, 
    Type Interface, 
    ServiceLifetime Lifetime
);

