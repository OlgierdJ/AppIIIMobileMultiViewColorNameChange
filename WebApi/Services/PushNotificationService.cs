using Microsoft.AspNetCore.SignalR;
using CoreLib.Interfaces;
using CoreLib.Enums;
using CoreLib.Interfaces.Hubs;
using CoreLib.Models;
using TodoWebApi.Hubs;

namespace TodoWebApi.Services
{
    public class PushNotificationService : IPushNotificationService
    {
        protected readonly IHubContext<ClientHub, IClientHubClient> _hubContext;
        private Dictionary<string, Func<IEntity, EntityAction, Task>> _hubManager = new();
        public PushNotificationService(IHubContext<ClientHub, IClientHubClient> hubContext)
        {
            _hubContext = hubContext;
            _hubManager.Add(nameof(Individual), async (entity, action) => { await _hubContext.Clients.All.ReceiveIndividualUpdateMessage((Individual)entity, action); });
             }

        public async Task NotifyClients<T>(T entity, EntityAction action) where T : IEntity
        {
            await Task.Run(async()=>
            {
                var task = _hubManager[typeof(T).Name];
                await task(entity, action);
            });
        }

        public Task NotifyClients<T>(IEnumerable<T> entities, EntityAction action) where T : IEntity
        {
            throw new NotImplementedException();
        }
    }
}
