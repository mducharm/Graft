using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PlotNet.Models;

namespace PlotNet;

public class PlotNetMiddleware
{
    private readonly PlotNetOptions _options;
    private readonly StaticFileMiddleware _staticFileMiddleware;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly XmlSerializer _xmlSerializer;

    enum Routes { Redirect, Index, JSON, XML }

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

        _xmlSerializer = new XmlSerializer(typeof(GraphDTO));
    }

    public async Task Invoke(HttpContext httpContext)
    {
        var httpMethod = httpContext.Request.Method;
        var path = httpContext.Request.Path.Value ?? "";

        var route = GetRoute(path);

        if (httpMethod == "GET")
        {

            if (route == Routes.Redirect)
            {
                var relativeIndexUrl = string.IsNullOrEmpty(path) || path.EndsWith("/")
                    ? "index.html"
                    : $"{path.Split('/').Last()}/index.html";

                httpContext.Response.StatusCode = 301;
                httpContext.Response.Headers["Location"] = relativeIndexUrl;
                return;
            }
            if (route == Routes.Index)
            {
                await RespondWithIndexHtml(httpContext.Response);
                return;
            }
            if (route == Routes.JSON)
            {
                await RespondWithJSON(httpContext.Response);
                return;
            }
            if (route == Routes.XML)
            {
                await RespondWithXML(httpContext.Response);
                return;
            }

        }

        await _staticFileMiddleware.Invoke(httpContext);

    }

    private async Task RespondWithXML(HttpResponse response)
    {
        response.StatusCode = 200;
        response.ContentType = "text/xml;charset=utf-8";

        GraphDTO graphDTO = PlotNetGenerator.GeneratePayload(_options.Services);
        
        using var sww = new StringWriter();
        using XmlWriter writer = XmlWriter.Create(sww);

        _xmlSerializer.Serialize(writer, graphDTO);
        string? graphPayload = sww.ToString() ?? "";

        await response.WriteAsync(graphPayload, Encoding.UTF8);
    }

    private async Task RespondWithJSON(HttpResponse response)
    {
        response.StatusCode = 200;
        response.ContentType = "text/json;charset=utf-8";

        GraphDTO graphDTO = PlotNetGenerator.GeneratePayload(_options.Services);

        var graphPayload = JsonSerializer.Serialize(graphDTO, _jsonSerializerOptions);

        await response.WriteAsync(graphPayload, Encoding.UTF8);
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
            FileProvider = new ManifestEmbeddedFileProvider(typeof(PlotNetMiddleware).GetTypeInfo().Assembly),
        };

        return new StaticFileMiddleware(next, hostingEnv, Options.Create(staticFileOptions), loggerFactory);
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

    private Routes? GetRoute(string path = "")
    {

        if (Regex.IsMatch(path, $"^/?{Regex.Escape(_options.RoutePrefix)}/?$", RegexOptions.IgnoreCase))
            return Routes.Redirect;

        if (Regex.IsMatch(path, $"^/{Regex.Escape(_options.RoutePrefix)}/?index.html$", RegexOptions.IgnoreCase))
            return Routes.Index;

        if (Regex.IsMatch(path, $"^/{Regex.Escape(_options.RoutePrefix)}.json/?$", RegexOptions.IgnoreCase))
            return Routes.JSON;

        if (Regex.IsMatch(path, $"^/{Regex.Escape(_options.RoutePrefix)}.xml/?$", RegexOptions.IgnoreCase))
            return Routes.XML;

        return null;
    }
}
