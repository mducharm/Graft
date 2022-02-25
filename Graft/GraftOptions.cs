using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Graft;

public class GraftOptions
{
    public string RoutePrefix { get; set; } = "graft";
    public IServiceCollection? Services { get; set; }
    public string DocumentTitle { get; set; } = "Graft";
    public string HeadContent { get; set; } = "";
    public Func<Stream?> IndexStream { get; set; } = () => typeof(GraftOptions)
            .GetTypeInfo()
            .Assembly
            .GetManifestResourceStream("Graft.index.html");
}
