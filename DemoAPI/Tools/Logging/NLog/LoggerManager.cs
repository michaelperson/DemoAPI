using DemoAPI.Tools.Logging.Interfaces; 
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAPI.Tools.Logging.NLog
{
    /// <summary>
    /// Implémentation de ma classe permettant d'utiliser l'interface Ilogger (.net core) pour
    /// enregistrer mes logs via NLOG
    /// </summary>
    public class LoggerManager : ILoggerManager
    {

        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public void LogCritic(string message)
        {
            //Remarque: POssibilité d'utiliser l'une des 17 signature pour personaliser le logging
            logger.Fatal(message);
        }

        public void LogDebug(string message)
        {
            logger.Debug(message);
        }

        public void LogError(string message)
        {
            logger.Error(message);
        }

        public void LogInfo(string message)
        {
            logger.Info(message);
        }

        public void LogWarn(string message)
        {
            logger.Warn(message);
        }
    }
}
