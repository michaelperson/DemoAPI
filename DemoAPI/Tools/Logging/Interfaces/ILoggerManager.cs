using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAPI.Tools.Logging.Interfaces
{
    /// <summary>
    /// Interface définissant les méthodes obligatoires pour mon système de log
    /// </summary>
    public interface ILoggerManager
    {
        void LogInfo(string message);
        void LogWarn(string message);
        void LogDebug(string message);
        void LogError(string message);
        void LogCritic(string message); 
    }
}
