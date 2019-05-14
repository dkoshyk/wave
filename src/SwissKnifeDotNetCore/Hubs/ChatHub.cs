using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace RealTimeMessenger.Hubs
{
    public class ChatHub : Hub
    {
        public async Task BroadcastChatData(int data) => await Clients.All.SendAsync("broadcastchatdata", data);
    }
}
