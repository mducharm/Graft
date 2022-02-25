using Microsoft.Extensions.DependencyInjection;

namespace Graft.Models;

public record ServiceData(
    Type? Implementation, 
    Type Interface, 
    ServiceLifetime Lifetime
);

