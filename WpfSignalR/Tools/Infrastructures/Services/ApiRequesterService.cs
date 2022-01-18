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

        public bool Login(LoginModel lm)
        {

            Task t = this.Execute("/Account/Login", JsonConvert.SerializeObject(lm), Method.POST, true);
            t.Wait();
            return t.IsCompletedSuccessfully;
        }
    }
}
