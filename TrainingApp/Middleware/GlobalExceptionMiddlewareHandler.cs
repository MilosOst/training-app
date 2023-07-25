using System.Text.Json;
using TrainingApp.APIResponses;
using TrainingApp.Exceptions;

namespace TrainingApp.Middleware;

public class GlobalExceptionMiddlewareHandler: IMiddleware
{
    private readonly ILogger<GlobalExceptionMiddlewareHandler> _logger;
    
    public GlobalExceptionMiddlewareHandler(ILogger<GlobalExceptionMiddlewareHandler> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }

    private async Task HandleException(HttpContext context, Exception ex)
    {
        _logger.LogError(ex, ex.Message);

        int statusCode = StatusCodes.Status500InternalServerError;
        string message = "Sorry, something went wrong.";

        switch (ex)
        {
            case UnauthorizedException:
                statusCode = 401;
                message = ex.Message;
                break;
            case ConflictException:
                statusCode = 409;
                message = ex.Message;
                break;
        }

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        
        MessageResponse body = new () { Message = message };
        string json = JsonSerializer.Serialize(body);
        await context.Response.WriteAsync(json);
    }
}