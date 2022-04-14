using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace WebsocketServer.Middleware;
public class WebSocketServerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly WebSocketServerConnectionManager _manager;
    public WebSocketServerMiddleware(RequestDelegate next, WebSocketServerConnectionManager manager)
    {
        _next = next;
        _manager = manager;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            WebSocket WebSocket = await context.WebSockets.AcceptWebSocketAsync();
            Debug.WriteLine("--> WebSocket Connected.");

            string connID = _manager.AddSocket(WebSocket);
            await SendConnID(WebSocket, connID);

            await ReciveMessage(WebSocket, async (result, buffer) =>
            {
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    Debug.WriteLine($"--> Message Recived: {Encoding.UTF8.GetString(buffer, 0, result.Count)}");
                    await RouteJsonMessage(Encoding.UTF8.GetString(buffer, 0, result.Count));
                    return;
                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    Debug.WriteLine("--> Recive Close Message.");
                    string id = _manager.GetAllSockets().Where(x => x.Value == WebSocket).Select(x => x.Key).FirstOrDefault();
                    _manager.GetAllSockets().TryRemove(id, out WebSocket sock);
                    await sock.CloseAsync(closeStatus: result.CloseStatus.Value,
                    statusDescription: result.CloseStatusDescription,
                    cancellationToken: CancellationToken.None);
                    return;
                }
            });
        }
        else
        {
            Debug.WriteLine("--> Http Request.");
            await _next(context);
        }
    }
    private static async Task SendConnID(WebSocket socket, string connID)
    {
        var buffer = Encoding.UTF8.GetBytes($"ConnID: {connID}");
        await socket.SendAsync(buffer: buffer, WebSocketMessageType.Text, endOfMessage: true, cancellationToken: CancellationToken.None);
    }
    private static async Task ReciveMessage(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
    {
        var buffer = new byte[1024 * 4];
        while (socket.State == WebSocketState.Open)
        {
            var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer), cancellationToken: CancellationToken.None);
            handleMessage(result, buffer);
        }
    }
    public async Task RouteJsonMessage(string message)
    {
        var routObj = JsonSerializer.Deserialize<WebSocketMessageObj>(message);

        if (Guid.TryParse(routObj.To, out Guid guidObj))
        {
            Debug.WriteLine("--> Targeted");
            var sock = _manager.GetAllSockets().FirstOrDefault(x => x.Key == routObj.To);
            if (sock.Value is not null)
            {
                if (sock.Value.State == WebSocketState.Open)
                {
                    await sock.Value.SendAsync(
                        buffer: Encoding.UTF8.GetBytes(routObj.Message),
                        messageType: WebSocketMessageType.Text,
                        endOfMessage: true,
                        cancellationToken: CancellationToken.None
                    );
                }
                else
                {
                    Debug.WriteLine("--> targeted socket is closed");
                }
            }
            else
            {
                Debug.WriteLine($"--> No Socket founded by {routObj.To}");
            }
        }
        else
        {
            Debug.WriteLine("--> Broadcast");
            foreach (var sock in _manager.GetAllSockets())
            {
                if (sock.Value.State == WebSocketState.Open)
                {
                    await sock.Value.SendAsync(
                        buffer: Encoding.UTF8.GetBytes(routObj.Message),
                        messageType: WebSocketMessageType.Text,
                        endOfMessage: true,
                        cancellationToken: CancellationToken.None
                    );
                }
            }
        }
    }
}

public class WebSocketMessageObj
{
    public string From { get; set; }
    public string To { get; set; }
    public string Message { get; set; }
}