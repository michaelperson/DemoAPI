using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfSignalR.Models;
using WpfSignalR.Tools.Infrastructures.Interfaces;

namespace WpfSignalR.Tools.Infrastructures.Services
{
    public class ApiRequesterService : RequesterService, IApiRequesterService
    {
        string _jwt;

        public string Jwt { get => _jwt;  }

        public ApiRequesterService(string baseUrl) : base(baseUrl)
        {
        }

        public async Task<bool> Login(LoginModel lm)
        {
           await this.Execute("/api/Account/Login", JsonConvert.SerializeObject(lm), Method.POST, true).ContinueWith
                ((obj)=> { return true; });

            return false;
        }
    }
}
