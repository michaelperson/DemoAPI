using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfSignalR.Models;

namespace WpfSignalR.Tools.Infrastructures.Interfaces
{
    public interface IApiRequesterService
    {
        bool Login(LoginModel lm);
    }
}
