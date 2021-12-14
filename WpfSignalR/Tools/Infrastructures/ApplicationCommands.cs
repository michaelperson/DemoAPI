using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSignalR.Tools.Infrastructures
{
    public static class ApplicationCommands
    {
        public static CompositeCommand ShowFlyoutCommand = new CompositeCommand();
    }

    public interface IApplicationCommands
    {
        CompositeCommand ShowFlyoutCommand { get; }
    }

    public class ApplicationCommandsProxy : IApplicationCommands
    {
        public CompositeCommand ShowFlyoutCommand
        {
            get { return ApplicationCommands.ShowFlyoutCommand; }
        }
    }
}
