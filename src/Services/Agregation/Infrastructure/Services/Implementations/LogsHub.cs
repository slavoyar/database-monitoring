using Agregation.Domain.Intefaces;
using Agregation.Infrastructure.Services.DTO;
using Microsoft.AspNetCore.SignalR;

namespace Agregation.Infrastructure.Services.Implementations
{
    public class ServerStateHub : Hub
    {
        public async Task Send(string username, string message)
        {
            await Clients.All.SendAsync("ServersState", username, message);
        }
    }
}