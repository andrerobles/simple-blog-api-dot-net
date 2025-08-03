using System.Net.WebSockets;
using System.Text;
using System.Collections.Concurrent;

namespace simple_blog_api_dot_net.WebSockets;

public class PostWebSocketHandler
{
    private readonly ConcurrentBag<WebSocket> _webSockets = new();

    public void AddSocket(WebSocket socket)
    {
        _webSockets.Add(socket);
    }

    public async Task BroadcastAsync(string message)
    {
        var buffer = Encoding.UTF8.GetBytes(message);

        foreach (var socket in _webSockets)
        {
            if (socket.State == WebSocketState.Open)
            {
                await socket.SendAsync(
                    new ArraySegment<byte>(buffer, 0, buffer.Length),
                    WebSocketMessageType.Text,
                    true,
                    CancellationToken.None
                );
            }
        }
    }

    public void RemoveClosed()
    {
        // Limpeza opcional: remover sockets fechados
        // Pode enriquecer para thread safety conforme necessidade
    }
}
