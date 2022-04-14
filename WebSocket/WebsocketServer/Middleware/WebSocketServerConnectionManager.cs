using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net.WebSockets;

namespace WebsocketServer.Middleware;
public class WebSocketServerConnectionManager
{
    public ConcurrentDictionary<string, WebSocket> _sockets = new();
    public ConcurrentDictionary<string, WebSocket> GetAllSockets()
    {
        return _sockets;
    }
    public string AddSocket(WebSocket socket)
    {
        string connID = Guid.NewGuid().ToString();
        if(_sockets.TryAdd(connID,socket))
            Debug.WriteLine($"--> Connection Added: {connID}");
        else
            Debug.WriteLine("--> Could Not Add Connection");
        return connID;
    }
}