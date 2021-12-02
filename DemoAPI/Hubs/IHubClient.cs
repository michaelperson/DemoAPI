using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAPI.Hubs
{
    public interface IHubClient
    {
        void FnClientMessage(string user, string message);
        void Deconnexion();
        void Connexion();
    }
}
