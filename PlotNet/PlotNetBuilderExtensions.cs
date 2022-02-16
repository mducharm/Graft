namespace PlotNet;
public static class PlotNetBuilderExtensions
{
    public static IApplicationBuilder UsePlotNet(this IApplicationBuilder app, IServiceCollection services)
    {
        PlotNetOptions options = new();
        options.Services = services;

        return app.UsePlotNet(options);
    }

    public static IApplicationBuilder UsePlotNet(this IApplicationBuilder app, PlotNetOptions options)
    {
        return app.UseMiddleware<PlotNetMiddleware>(options);
    }

}
