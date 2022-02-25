<p align="center">
  <img src="./Graft.UI/src/assets/Graft-logos.jpeg" width="150" alt="Graft Logo">
</p>

# Graft

Like Swagger, but for visualizing your dependencies. Powered by Svelte + Cytoscape.js + .NET 6.

<p align="center">
  <img src="./.github/example.png" width="150" alt="Graft Example">
  <img src="./.github/example2.png" width="150" alt="Graft Example">
</p>

## Usage

An example using the .NET 6 minimal APIs:

```csharp
using Graft;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IServiceA, ServiceA>();
builder.Services.AddTransient<IServiceB, ServiceB>();

var app = builder.Build();

app.UseGraft(builder.Services);
```