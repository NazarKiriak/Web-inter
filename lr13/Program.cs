using Lab13.Exporters;
using Lab13.Processors;
using Lab13.Samplers;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenTelemetry()
    .WithMetrics(opt =>
    {
        opt
        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("Lab13"))
        .AddMeter("Lab13")
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddRuntimeInstrumentation()
        .AddProcessInstrumentation()
        .AddPrometheusExporter()
        .AddConsoleExporter()
        .AddOtlpExporter(opts =>
        {
            opts.Endpoint = new Uri(builder.Configuration["Otel:Endpoint"]);
        });
    })
    .WithTracing(trc =>
    {
        trc
        .AddAspNetCoreInstrumentation(opts =>
        {
            opts.EnrichWithHttpRequest = (activity, request) =>
            {
                if (request is HttpRequest httpRequest)
                {
                    activity.SetTag("user.id", "12345");
                    activity.SetBaggage("request.id", "abc12345");
                    activity.SetTag("http.method", httpRequest.Method);
                    activity.SetTag("http.url", httpRequest.Path);
                }
            };
        })
        .SetSampler(new AlwaysOnSampler())
        .SetErrorStatusOnException()
        .AddConsoleExporter();
    });


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var resourceBuilder = ResourceBuilder.CreateDefault()
    .AddService("Lab13");

using var tracerProvider = Sdk.CreateTracerProviderBuilder()
    .SetSampler(new MySampler(0.5))
    .AddSource("Lab13")
    .SetResourceBuilder(resourceBuilder)
    .AddProcessor(new MyProcessor())
    .AddProcessor(new SimpleActivityExportProcessor(new MyExporter("https://localhost:7089/weather/")))
    .Build();

tracerProvider.Dispose();

using var meterProvider = Sdk.CreateMeterProviderBuilder()
    .AddMeter("Lab13")
    .SetResourceBuilder(resourceBuilder)
    .AddConsoleExporter()
    .Build();

meterProvider.Dispose();

app.UseOpenTelemetryPrometheusScrapingEndpoint();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Welcome to Weather Service!");
    });

    endpoints.MapGet("/weather/{city}", async context =>
    {
        var city = context.Request.RouteValues["city"]?.ToString();
        if (string.IsNullOrEmpty(city))
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("City parameter is missing.");
            return;
        }

        var weather = GetWeather(city);
        await context.Response.WriteAsync($"Weather in {city}: {weather}");
    });
});

static string GetWeather(string city)
{
    switch (city.ToLower())
    {
        case "new york":
            return "Sunny";
        case "london":
            return "Cloudy";
        case "tokyo":
            return "Rainy";
        case "mykolaiv":
            return "Warm";
        case "canada":
            return "Cool";
        case "rio de janeiro":
            return "Hot";
        default:
            return "Unknown";
    }
}

app.MapControllers();

app.Run();
