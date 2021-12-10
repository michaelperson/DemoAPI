using BLL.Interface;
using BLL.Models;
using BLL.Services;
using DAL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using SignalRTool.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRTool
{
    public class SignalRMngt<T> : ISignalRMngt
        where T : class
    {
        private readonly Hub<T> _hub;
         
        public SignalRMngt(Hub<T> hub)
        {
            this._hub = hub; 
        }

        #region Tools
        public string ExtractEmailFromJwt()
        {
            HttpContext httpContext = this._hub.Context.GetHttpContext();
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

        #endregion

        #region ToDb
        public BusinessUser FromConnectionId(string SignalRConnectionId, IUserRepository ur)
        {
            BusinessUserService bu = new BusinessUserService(ur);
            return bu.GetBySignalrConnectionId(SignalRConnectionId);
        }

        public string GetConnectionId(int UserId, IUserRepository ur)
        {
            BusinessUserService bu = new BusinessUserService(ur);
            return bu.GetById(UserId)?.SignalRConnectionId;
        }
        #endregion

        #region Manage Groups
        public IEnumerable<string> GetGroups(int userId, IGroupesBusiness<BusinessGroupes> gr)
        {
           return gr.GetByUser(userId).Select(g=>g.Nom);
        }

        #endregion
    }
}
