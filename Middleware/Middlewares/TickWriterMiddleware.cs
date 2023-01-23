namespace Middleware.Middlewares;

public class TickWriterMiddleware
{
    private readonly RequestDelegate _next;

    public TickWriterMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    // IMessageWriter is injected into InvokeAsync
    public async Task InvokeAsync(HttpContext httpContext, MessageWriter svc)
    {
        Console.WriteLine($"start running {nameof(TickWriterMiddleware)}.");
        svc.Write(DateTime.Now.Ticks.ToString());
        Console.WriteLine($"{nameof(TickWriterMiddleware)} finished and invoke next.");
        await _next(httpContext);
    }
}

public static class TickWriterMiddlewareExtensions
{
    public static IApplicationBuilder UseTickWriter(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TickWriterMiddleware>();
    }
}

public class MessageWriter
{
    public void Write(string message)
    {
        Console.WriteLine(message);
    }
}