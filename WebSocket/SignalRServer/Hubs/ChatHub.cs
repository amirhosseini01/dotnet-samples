using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.SignalR;

namespace SignalRServer.Hubs;
public class ChatHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        Debug.WriteLine($"--> Connection Established {Context.ConnectionId}");
        await Clients.Client(Context.ConnectionId).SendAsync(method: "ReceiveConnID", arg1: Context.ConnectionId);
        await base.OnConnectedAsync();
    }
    public async Task SendMessageAsync(string message)
    {
        var routObj = JsonSerializer.Deserialize<MessageObj>(message);
        string toClient = routObj.To;
        Debug.WriteLine($"--> Message Received on: {Context.ConnectionId}");

        if (string.IsNullOrEmpty(toClient))
        {
            await Clients.All.SendAsync(method: "ReceiveMessage", arg1: routObj.Message);
        }
        else
        {
            await Clients.Client(routObj.To).SendAsync(method: "ReceiveMessage", arg1: routObj.Message);
        }
    }
}
public class MessageObj
{
    public string From { get; set; }
    public string To { get; set; }
    public string Message { get; set; }
}