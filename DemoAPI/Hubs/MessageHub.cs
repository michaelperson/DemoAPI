using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            HttpContext httpContext = this.Context.GetHttpContext();

            string authHeader = httpContext.Request.Headers["Authorization"];
            if(!string.IsNullOrEmpty(authHeader))
            {
                string bearer = authHeader.Remove(0, 6).Trim();
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(bearer);
                JwtSecurityToken tokenS = jsonToken as JwtSecurityToken;
                string email = tokenS.Payload["email"].ToString();
                //Add connectionId to Db if not exist

            }
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
