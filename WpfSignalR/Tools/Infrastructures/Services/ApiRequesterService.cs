using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfSignalR.Models;
using WpfSignalR.Tools.Infrastructures.Interfaces;

namespace WpfSignalR.Tools.Infrastructures.Services
{
    public class ApiRequesterService : RequesterService, IApiRequesterService
    {
         

        public ApiRequesterService(string baseUrl) : base(baseUrl)
        {
        }

        public bool Login(LoginModel lm)
        {

            Task t = this.Execute("/Account/Login", JsonConvert.SerializeObject(lm), Method.POST, true);
            t.Wait();
            if (t.IsCanceled || t.IsFaulted) return false;
            else
             
                return t.IsCompletedSuccessfully;
        }

        public UserModel GetConnectedUserInfo()
        {
            if (this.Token == null) return null;
            //Extract Id from Jwt
            string jwt = Security.SecurityTools.SecureStringToString(this.Token);
            JwtSecurityTokenHandler Handler = new JwtSecurityTokenHandler();
            SecurityToken token =Handler.ReadToken(jwt.Replace("\"",""));
            
            

            return default(UserModel);

        }
    }
}
