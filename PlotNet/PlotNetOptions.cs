using System.Reflection;

namespace PlotNet;

public class PlotNetOptions
{
    /// <summary>
    /// Gets or sets a route prefix for accessing the swagger-ui
    /// </summary>
    public string RoutePrefix { get; set; } = "plotnet";

    public IServiceCollection? Services { get; set; }

    public string DocumentTitle { get; set; } = "PlotNet";
    public string HeadContent { get; set; } = "";
    public Func<Stream?> IndexStream { get; set; } = () => typeof(PlotNetOptions)
            .GetTypeInfo()
            .Assembly
            .GetManifestResourceStream("Swashbuckle.AspNetCore.SwaggerUI.index.html");
}
