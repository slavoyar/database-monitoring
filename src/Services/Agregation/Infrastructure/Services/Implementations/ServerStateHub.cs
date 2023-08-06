using Agregation.Domain.Intefaces;
using Agregation.Domain.Interfaces;
using Agregation.Infrastructure.Services.DTO;
using Microsoft.AspNetCore.SignalR;

namespace Agregation.Infrastructure.Services.Implementations
{
    public class ServerStateHub : Hub
    {
        public async Task Subscribe(string serverId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, serverId);
        }

        public async Task SubscribeToGroup(ICollection<string> ListServerId)
        {
            foreach (var serverId in ListServerId)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, serverId);
            }
        }

        public async Task Unsubscribe(ICollection<string> ListServerId)
        {
            foreach (var serverId in ListServerId)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, serverId);
            }
        }

        public async Task UnsubscribeToGroup(string serverId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, serverId);

        }

        public async Task Send(string username, string message)
        {
            await Clients.All.SendAsync("Receive", username, message);
        }

        public async Task SendGroup(string serverId, string message)
        {
            await Clients.Group(serverId).SendAsync("Receive", message);
        }
    }
}