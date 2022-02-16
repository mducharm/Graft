using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

namespace PlotNet;

public class PlotNetMiddleware
{
    private const string EmbeddedFileNamespace = "PlotNetMiddleware";

    private readonly PlotNetOptions _options;
    private readonly StaticFileMiddleware _staticFileMiddleware;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public PlotNetMiddleware(
        RequestDelegate next,
        IWebHostEnvironment webHostEnvironment,
        ILoggerFactory loggerFactory,
        PlotNetOptions options
    )
    {
        _options = options ?? new PlotNetOptions();

        _staticFileMiddleware = CreateStaticFileMiddleware(next, webHostEnvironment, _options, loggerFactory);

        _jsonSerializerOptions = new();

#if NET6_0
        _jsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
#else
        _jsonSerializerOptions.IgnoreNullValues = true;
#endif
        _jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        _jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, false));
    }

    public async Task Invoke(HttpContext httpContext)
    {
        var httpMethod = httpContext.Request.Method;
        var path = httpContext.Request.Path.Value ?? "";

        // If the RoutePrefix is requested (with or without trailing slash), redirect to index URL
        if (httpMethod == "GET" && Regex.IsMatch(path, $"^/?{Regex.Escape(_options.RoutePrefix)}/?$", RegexOptions.IgnoreCase))
        {
            // Use relative redirect to support proxy environments
            var relativeIndexUrl = string.IsNullOrEmpty(path) || path.EndsWith("/")
                ? "index.html"
                : $"{path.Split('/').Last()}/index.html";

            RespondWithRedirect(httpContext.Response, relativeIndexUrl);
            return;
        }

        if (httpMethod == "GET" && Regex.IsMatch(path, $"^/{Regex.Escape(_options.RoutePrefix)}/?index.html$", RegexOptions.IgnoreCase))
        {
            await RespondWithIndexHtml(httpContext.Response);
            return;
        }

        await _staticFileMiddleware.Invoke(httpContext);
    }


    private static StaticFileMiddleware CreateStaticFileMiddleware(
        RequestDelegate next,
        IWebHostEnvironment hostingEnv,
        PlotNetOptions options,
        ILoggerFactory loggerFactory
        )
    {
        var staticFileOptions = new StaticFileOptions
        {
            RequestPath = string.IsNullOrEmpty(options.RoutePrefix) ? string.Empty : $"/{options.RoutePrefix}",
            FileProvider = new EmbeddedFileProvider(typeof(PlotNetMiddleware).GetTypeInfo().Assembly, EmbeddedFileNamespace),
        };

        return new StaticFileMiddleware(next, hostingEnv, Options.Create(staticFileOptions), loggerFactory);
    }

    private void RespondWithRedirect(HttpResponse response, string location)
    {
        response.StatusCode = 301;
        response.Headers["Location"] = location;
    }

    private async Task RespondWithIndexHtml(HttpResponse response)
    {
        response.StatusCode = 200;
        response.ContentType = "text/html;charset=utf-8";

        using var stream = _options.IndexStream();
        using var reader = new StreamReader(stream ?? throw new FileNotFoundException("unable to find index.html"));

        // Inject arguments before writing to response
        var htmlBuilder = new StringBuilder(await reader.ReadToEndAsync());

        foreach (var entry in GetIndexArguments())
            htmlBuilder.Replace(entry.Key, entry.Value);

        await response.WriteAsync(htmlBuilder.ToString(), Encoding.UTF8);
    }

    private IDictionary<string, string> GetIndexArguments()
    {
        return new Dictionary<string, string>()
            {
                { "%(DocumentTitle)", _options.DocumentTitle },
                { "%(HeadContent)", _options.HeadContent },
                // { "%(ConfigObject)", JsonSerializer.Serialize(_options.ConfigObject, _jsonSerializerOptions) },
            };
    }

}
