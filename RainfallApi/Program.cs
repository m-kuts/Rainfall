using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using RainfallApi.Services.Contracts;
using RainfallApi.DTOs.Error;
using RainfallApi.Services.Implementations;
using RainfallApi.Middlewares.Exceptions;

var builder = WebApplication.CreateBuilder(args);

ConfigureControllers(builder.Services);
ConfigureServices(builder.Services);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "1.0",
        Title = "Rainfall API",
        Description = "An API which provides rainfall reading data",
        Contact = new OpenApiContact
        {
            Name = "Sorted",
            Url = new Uri("https://www.sorted.com"),
        },
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

#region Configuration

void ConfigureServices(IServiceCollection services)
{
    services.AddTransient<ExceptionMiddleware>();
    services.AddTransient<IRainfallService, RainfallService>();
}

void ConfigureControllers(IServiceCollection services)
{
    services.AddControllers().ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            return new BadRequestObjectResult(
                new ErrorResponse
                {
                    Message = "Invalid request",
                    Detail = context
                                .ModelState
                                .Where(e => e.Value?.Errors.Any() ?? false)
                                .Select(e => new ErrorDetail
                                {
                                    PropertyName = e.Key,
                                    Message = string.Join(';', e.Value?.Errors.Select(err => err.ErrorMessage) ?? Array.Empty<string>())
                                })
                });
        };
    });
}

#endregion