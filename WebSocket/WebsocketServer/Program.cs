using WebsocketServer.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddWebSocketManager();
var app = builder.Build();

app.UseWebSockets();
app.UseWebsocketServer();

app.MapGet("/", () => "Hello World!");
app.Run();