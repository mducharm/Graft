using System.Reflection;
using Graft;
using Graft.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IServiceA, ServiceA>();
builder.Services.AddTransient<IServiceB, ServiceB>();
builder.Services.AddTransient<IServiceC, ServiceC>();
builder.Services.AddTransient<IServiceD, ServiceD>();

ServiceCollection services = new();

services.AddTransient<IServiceA, ServiceA>();
services.AddTransient<IServiceB, ServiceB>();
services.AddTransient<IServiceC, ServiceC>();
services.AddTransient<IServiceD, ServiceD>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseGraft(services);

app.Run();
