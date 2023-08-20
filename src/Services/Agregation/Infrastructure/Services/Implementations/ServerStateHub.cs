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

        public async Task UnsubscribeToGroup(ICollection<Guid> ListServerId)
        {
            foreach (var serverId in ListServerId)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, serverId.ToString());
            }
        }

        public async Task Unsubscribe(Guid serverId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, serverId.ToString());

        }
    }
}