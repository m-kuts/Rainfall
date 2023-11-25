using RainfallApi.DTOs.Error;
using RainfallApi.Exceptions;

namespace RainfallApi.Middlewares.Exceptions;

public class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (StatusCodeException ex)
        {
            var response = context.Response;
            if (!response.HasStarted)
            {
                response.ContentType = "application/json";
                response.StatusCode = (int)ex.StatusCode;
                await response.WriteAsJsonAsync(new ErrorResponse
                {
                    Message = ex.Message
                });
            }
            else
            {
                _logger.LogError("Unable to send exception response for exception: {0}, trace: {1}", ex.Message, ex.StackTrace);
            }
        }
        catch (Exception ex)
        {
            if (ex.InnerException != null)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
            }

            var response = context.Response;
            if (!response.HasStarted)
            {
                response.ContentType = "application/json";
                response.StatusCode = 500;
                await response.WriteAsJsonAsync(new ErrorResponse
                {
                    Message = "Internal server error"
                });
            }
            else
            {
                _logger.LogError("Unable to send exception response for exception: {0}, trace: {1}", ex.Message, ex.StackTrace);
            }
        }
    }
}
