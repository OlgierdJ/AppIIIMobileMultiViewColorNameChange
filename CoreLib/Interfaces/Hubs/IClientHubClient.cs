using CoreLib.Enums;
using CoreLib.Models;

namespace CoreLib.Interfaces.Hubs
{
    public interface IClientHubClient
    {
        Task Login(string user);
        Task JoinGroup(string groupName);
        Task LeaveGroup(string groupName);
        Task LeaveGroups(string[] groupNames);
        Task OnConnectedAsync();
        Task ReceiveIndividualUpdateMessage(Individual entity, EntityAction action);

        Task ReceiveUpdateMessage(string message);
    }
}