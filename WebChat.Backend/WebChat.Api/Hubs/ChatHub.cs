using Microsoft.AspNetCore.SignalR;

namespace WebChat.Api.Hubs;

public class ChatHub : Hub
{
    public async Task JoinRoom(string roomName)
    {
        await Groups.AddToGroupAsync(this.Context.ConnectionId, roomName);
    }
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public async Task SendGroupMessage(string senderUsername, string senderId, string message, string roomName)
    {
        Console.WriteLine($"{senderUsername}: {message} in {roomName}");
        await Clients.Group(roomName).SendAsync("ReceiveGroupMessage", senderUsername, senderId, message, roomName);
    }

    public async Task SendGroupMessageDeleted(string senderUsername, string senderId, string messageId, string roomName)
    {
        await Clients.Group(roomName).SendAsync("ReceiveGroupMessageDeleted", senderUsername, senderId, messageId, roomName);
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return base.OnDisconnectedAsync(exception);
    }
}