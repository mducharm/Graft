using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace PlotNet;

public class PlotNetOptions
{
    public string RoutePrefix { get; set; } = "plotnet";
    public IServiceCollection? Services { get; set; }
    public string DocumentTitle { get; set; } = "PlotNet";
    public string HeadContent { get; set; } = "";
    public Func<Stream?> IndexStream { get; set; } = () => typeof(PlotNetOptions)
            .GetTypeInfo()
            .Assembly
            .GetManifestResourceStream("PlotNet.index.html");
}
