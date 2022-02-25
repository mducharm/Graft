<p align="center">
  <img src="./Graft.UI/src/assets/Graft-logos.jpeg" width="150" alt="Graft Logo">
</p>

# Graft

Like Swagger, but for visualizing your dependencies. Powered by Svelte + Cytoscape.js + .NET 6.

- Provide your IServiceCollection to `app.UseGraft(services)` to inspect your dependencies.
- Show/hide dependencies through sidebar menu, or right-click to change layout.
- Color-coded based on [service lifetimes](https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection#service-lifetimes).

<p align="center">
  <img src="./.github/example.png" width="345" alt="Graft Example">
  <img src="./.github/example2.png" width="400" alt="Graft Layout Selection Example">
</p>

## Usage

An example using the .NET 6 minimal APIs:

```csharp
using Graft;

var builder = WebApplication.CreateBuilder(args);

IServiceCollection services = new();

services.AddTransient<IServiceA, ServiceA>();
services.AddTransient<IServiceB, ServiceB>();

var app = builder.Build();

app.UseGraft(services);
```