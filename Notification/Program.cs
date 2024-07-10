using Microsoft.OpenApi.Models;
using Notification;
using Notification.Middleware;
using Notification.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddServices(builder.Configuration);

builder.Services.AddControllers();
var policy = "localhost";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policy,
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowedToAllowWildcardSubdomains();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Notification", Version = "v1" });
    opt.EnableAnnotations();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(policy);

app.UseHttpsRedirection();

app.UseExceptionHandlerMiddleware();

app.MapControllers();

app.Services.InitializeInfrastructureServices();

app.Run();