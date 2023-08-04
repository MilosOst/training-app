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
            case BadRequestException badRequestEx:
                var response = new BadRequestResponse() { Errors = badRequestEx.Errors };
                string errorsJson = JsonSerializer.Serialize(response);
                
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(errorsJson);
                return;
            case UnauthorizedException:
                statusCode = 401;
                message = ex.Message;
                break;
            case ConflictException:
                statusCode = 409;
                message = ex.Message;
                break;
            case NotFoundException:
                statusCode = 404;
                message = ex.Message;
                break;
            case ForbiddenException:
                statusCode = 403;
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