using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAPI.Hubs
{
    /// <summary>
    /// Contract avec le Client pas avec notre classe Hub
    /// Définit les méthodes qui devront être présente du côté JS,Angular, Wpf,...
    /// Le but : UNIQUEMENT facilité l'écriture et non imposer les méthodes aux clients
    /// </summary>
    public interface IHubClient
    {
        Task FnClientMessage(string user, string message);
        Task Deconnexion();
        Task Connexion();
    }
}
