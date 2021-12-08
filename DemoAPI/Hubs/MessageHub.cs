using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BLL.Interface;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using SignalRTool;
using SignalRTool.Interfaces;

namespace DemoAPI.Hubs
{
    public class MessageHub: Hub<IHubClient>
    {
        IUserBusiness<BusinessUser> _userBusinessService;
        ISignalRMngt _signMngt;
        public MessageHub(IUserBusiness<BusinessUser> userBusinessService)
        {
            _userBusinessService = userBusinessService;
            _signMngt = new SignalRMngt<IHubClient>(this);
        }
        [Authorize] 
        public void SendMessage(string user, string message)
        {
              
              Clients.All.FnClientMessage( user, message);
        }

 

        public  override Task OnConnectedAsync()
        {
            string email = ExtractEmailFromJwt();


            if (!string.IsNullOrEmpty(email))
            {               
                BusinessUser bu = _userBusinessService.GetByEmail(email);
                bu.SignalRConnectionId = Context.ConnectionId;
                _userBusinessService.Update(bu);

                //Récupération des groupes du membre
                
            }
            Clients.Others.Connexion();
             

            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            Clients.Others.Deconnexion();

             LeaveGroup("Membre").Wait();
            string email = ExtractEmailFromJwt();

            if (!string.IsNullOrEmpty(email))
            {
                BusinessUser bu = _userBusinessService.GetByEmail(email);
                bu.SignalRConnectionId =null;
                _userBusinessService.Update(bu);

            }

            return base.OnDisconnectedAsync(exception);
        }
        public async Task JoinGroup(string GroupName)
        {
            await  Groups.AddToGroupAsync( Context.ConnectionId, GroupName);
            await  Clients.Groups(GroupName).FnClientMessage("Server", $"{ Context.ConnectionId} a rejoint le groupe {GroupName}");
        }
        public async Task LeaveGroup(string GroupName)
        {
            await  Groups.RemoveFromGroupAsync( Context.ConnectionId, GroupName);
            await  Clients.Group(GroupName).FnClientMessage("Server", $"{ Context.ConnectionId} a quitté le groupe {GroupName}");
        }


        private string ExtractEmailFromJwt()
        {
            HttpContext httpContext = this.Context.GetHttpContext();
            string email = null;
            string authHeader = httpContext.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authHeader))
            {
                string bearer = authHeader.Remove(0, 6).Trim();
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(bearer);
                JwtSecurityToken tokenS = jsonToken as JwtSecurityToken;
                email = tokenS.Payload["email"].ToString();
            }
            return email;
        }
    }
}
