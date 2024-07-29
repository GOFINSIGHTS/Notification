using Microsoft.OpenApi.Models;
using Notification;
using Notification.Middleware;
using Notification.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddServices(builder.Configuration);

builder.Services.AddControllers();

var devPolicy = "localhost";
var prodPolicy = "prodCorsPolicy";

var allowedOrigins = builder.Configuration.GetValue<string>("AllowedOrigins")?.Split(',');

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: devPolicy,
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowedToAllowWildcardSubdomains();
        });

    options.AddPolicy(name: prodPolicy,
        builder =>
        {
            if (allowedOrigins != null && allowedOrigins.Length > 0)
            {
                builder.WithOrigins(allowedOrigins)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowedToAllowWildcardSubdomains();
            }
            else
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Notification", Version = "v1" });
    opt.EnableAnnotations();
});

var app = builder.Build();

Console.WriteLine($"Current Environment: {app.Environment.EnvironmentName}");
Console.WriteLine($"Is Development: {app.Environment.IsDevelopment()}");
if (allowedOrigins is not null)
{
    foreach (var item in allowedOrigins)
    {
        Console.WriteLine(item);
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(devPolicy);
}
else
{
    app.UseCors(prodPolicy);
}

//if (!app.Environment.IsDevelopment())
//{
//    app.UseHttpsRedirection();
//}

app.UseExceptionHandlerMiddleware();

app.MapControllers();

app.Services.InitializeInfrastructureServices();

app.Run();