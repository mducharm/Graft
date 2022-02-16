using System.Reflection;
using PlotNet;
using PlotNet.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IServiceA, ServiceA>();
builder.Services.AddTransient<IServiceB, ServiceB>();

foreach (var service in builder.Services)
{
    // Console.WriteLine(service.ToString());
    List<ConstructorInfo>? info = service.ImplementationType?.GetConstructors().ToList() ?? new List<ConstructorInfo>();

    foreach (var i in info)
    {
        if (i.IsPublic)
        {
            Console.WriteLine($"constructor of {service.ImplementationType?.Name}");
            foreach (var p in i.GetParameters())
            {
                Console.WriteLine(p.Name);
                // Console.WriteLine(p.GetType().Name);
                // Console.WriteLine(p.ParameterType);

            }
                Console.WriteLine("___");
        }
    }


    Console.WriteLine(service.ImplementationType?.Name);
    Console.WriteLine(service.ServiceType.Name);
    Console.WriteLine(service.Lifetime);
}

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

app.UsePlotNet(builder.Services);
// app.UseStaticFiles(new StaticFileOptions
// {
//     // FileProvider = new PhysicalFileProvider(
//     FileProvider = new EmbeddedFileProvider(
//            Path.Combine(builder.Environment.ContentRootPath, "MyStaticFiles")),
//     RequestPath = "/StaticFiles"
// });

app.Run();
