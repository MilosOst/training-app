namespace TrainingApp.Middleware;

public class RequestLoggingMiddleware: IMiddleware
{
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(ILogger<RequestLoggingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        _logger.LogInformation($"Incoming Request: {context.Request.Method} {context.Request.Path}");
        await next(context);
        _logger.LogInformation($"[{context.Response.StatusCode}] - {context.Request.Method} {context.Request.Path}");
    }
}