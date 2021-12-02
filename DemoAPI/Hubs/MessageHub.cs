using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
namespace DemoAPI.Hubs
{
    public class MessageHub: Hub<IHubClient>
    {
        [Authorize] 
        public void SendMessage(string user, string message)
        {
              
              Clients.All.FnClientMessage( user, message);
        }

 

        public  override Task OnConnectedAsync()
        {
            Clients.Others.Connexion();
            JoinGroup("Membre").Wait();

            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            Clients.Others.Deconnexion();

            LeaveGroup("Membre").Wait();

            return base.OnDisconnectedAsync(exception);
        }

        public async Task JoinGroup(string GroupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, GroupName);
            await Clients.Group(GroupName).FnClientMessage("Server", $"{Context.ConnectionId} a rejoint le groupe {GroupName}");
        }
        public async Task LeaveGroup(string GroupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, GroupName);
            await Clients.Group(GroupName).FnClientMessage("Server", $"{Context.ConnectionId} a quitté le groupe {GroupName}");
        }
    }
}
