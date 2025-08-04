using simple_blog_api_dot_net.WebSockets;

namespace simple_blog_api_dot_net.Configurations
{
    public static class WebSocketConfig
    {
        public static WebApplication UsePostWebSocketEndpoint(this WebApplication app)
        {
            app.Map("/ws/posts", async context =>
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    var ws = await context.WebSockets.AcceptWebSocketAsync();
                    var wsHandler = context.RequestServices.GetRequiredService<PostWebSocketHandler>();
                    wsHandler.AddSocket(ws);

                    var buffer = new byte[1024 * 4];
                    while (ws.State == System.Net.WebSockets.WebSocketState.Open)
                    {
                        var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                        if (result.MessageType == System.Net.WebSockets.WebSocketMessageType.Close)
                        {
                            await ws.CloseAsync(System.Net.WebSockets.WebSocketCloseStatus.NormalClosure, "Closed by client", CancellationToken.None);
                            break;
                        }
                    }
                }
                else
                {
                    context.Response.StatusCode = 400;
                }
            });
            return app;
        }
    }
}