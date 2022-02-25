using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Graft;
public static class GraftBuilderExtensions
{
    public static IApplicationBuilder UseGraft(this IApplicationBuilder app, IServiceCollection services)
    {
        GraftOptions options = new();
        options.Services = services;

        return app.UseGraft(options);
    }

    public static IApplicationBuilder UseGraft(this IApplicationBuilder app, GraftOptions options)
    {
        return app.UseMiddleware<GraftMiddleware>(options);
    }

}
