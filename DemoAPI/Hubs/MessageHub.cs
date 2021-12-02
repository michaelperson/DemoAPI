using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
namespace DemoAPI.Hubs
{
    public class MessageHub: Hub<IHubClient>
    {
        public void SendMessage(string user, string message)
        {
              Clients.All.FnClientMessage( user, message);
        }

        public override  Task OnConnectedAsync()
        {
            Clients.Others.Connexion();
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            Clients.Others.Deconnexion();
            return base.OnDisconnectedAsync(exception);
        }
    }
}
