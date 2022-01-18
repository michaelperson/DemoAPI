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
using WpfSignalR.Tools.Infrastructures.Security;

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
            {
                SecurityTools.ExtractInfoFromJwt(this.Token);
                return t.IsCompletedSuccessfully;
            }
        }

         
    }
}
