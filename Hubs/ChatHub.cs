using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace EcommerseNextGenPlatform.Hubs
{
    public class ChatHub : Hub
    {
        private static ConcurrentDictionary<string, CancellationTokenSource> _timers = new();

        public override Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            var cts = new CancellationTokenSource();
            _timers[connectionId] = cts;

            _ = SendServerTimeLoop(connectionId, cts.Token);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var connectionId = Context.ConnectionId;
            if (_timers.TryRemove(connectionId, out var cts))
            {
                cts.Cancel();
                cts.Dispose();
            }

            return base.OnDisconnectedAsync(exception);
        }

        public async Task NewMessage(long username, string message)
        {
            await Clients.All.SendAsync("messageReceived", username, message);
        }

        
        

        private async Task SendServerTimeLoop(string connectionId, CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    string currentTime = DateTime.UtcNow.ToString("HH:mm:ss");

                    // This will throw if the connection is gone – we catch it to stop cleanly
                    try
                    {
                        await Clients.Client(connectionId).SendAsync("ServerTime", currentTime, cancellationToken: token);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error sending to {connectionId}: {ex.Message}");
                        break;
                    }

                    await Task.Delay(1000, token);
                }
            }
            catch (OperationCanceledException)
            {
                // Expected when the token is cancelled.
            }
        }
    }
}
