using Microsoft.AspNetCore.SignalR;
using CoreLib.Interfaces.Hubs;

namespace TodoWebApi.Hubs
{
    public class ClientHub : Hub<IClientHubClient>
    {
    }
}
