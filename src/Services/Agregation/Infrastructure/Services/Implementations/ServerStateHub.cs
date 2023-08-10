using Agregation.Domain.Intefaces;
using Agregation.Domain.Interfaces;
using Agregation.Infrastructure.Services.DTO;
using Microsoft.AspNetCore.SignalR;

namespace Agregation.Infrastructure.Services.Implementations
{
    public class ServerStateHub : Hub
    {
        public async Task Subscribe(Guid serverId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, serverId.ToString());
        }

        public async Task SubscribeToGroup(ICollection<Guid> ListServerId)
        {
            foreach (var serverId in ListServerId)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, serverId.ToString());
            }
        }

        public async Task Unsubscribe(ICollection<Guid> ListServerId)
        {
            foreach (var serverId in ListServerId)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, serverId.ToString());
            }
        }

        public async Task UnsubscribeToGroup(Guid serverId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, serverId.ToString());

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