namespace WebsocketServer.Middleware;
    public static class WebSocketServerMiddlewareExtentions
    {
        public static IApplicationBuilder UseWebsocketServer(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<WebSocketServerMiddleware>();
        }
        public static IServiceCollection AddWebSocketManager(this IServiceCollection services)
        {
            services.AddSingleton<WebSocketServerConnectionManager>();
            return services;
        }
    }