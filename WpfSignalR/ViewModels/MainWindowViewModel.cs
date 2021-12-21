using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfSignalR.Tools.Infrastructures.Base;
using WpfSignalR.Tools.Infrastructures.Events;

namespace WpfSignalR.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// CTOR
        /// </summary>
        public MainWindowViewModel()
        {
            // Register to events
            EventAggregator.GetEvent<StatusBarMessageUpdateEvent>().Subscribe(OnStatusBarMessageUpdateEvent);

            
        }

        #region Event-Handler

        /// <summary>
        /// EventHandler for the update status bar event
        /// </summary>
        /// <param name="statusBarMessage"></param>
        private void OnStatusBarMessageUpdateEvent(string statusBarMessage)
        {
            this.StatusBarMessage = statusBarMessage;
        }

        #endregion Event-Handler

        #region Properties

        private string statusBarMessage;

        /// <summary>
        /// Status-Bar message
        /// </summary>
        public string StatusBarMessage
        {
            get { return statusBarMessage; }
            set { this.SetProperty<string>(ref this.statusBarMessage, value); }
        }

        #endregion Properties
    }
}
