# PlotNet

Like Swagger, but for visualizing your dependencies. Powered by Svelte + Cytoscape.js + .NET 6.

## Usage

An example using the .NET 6 minimal APIs:

```csharp
using PlotNet;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IServiceA, ServiceA>();
builder.Services.AddTransient<IServiceB, ServiceB>();

var app = builder.Build();

app.UsePlotNet(builder.Services);
```