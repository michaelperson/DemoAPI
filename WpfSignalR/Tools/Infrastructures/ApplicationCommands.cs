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
        public static CompositeCommand NavigateToCommand = new CompositeCommand();
    }

    public interface IApplicationCommands
    {
        CompositeCommand ShowFlyoutCommand { get; }
        CompositeCommand NavigateToCommand { get; }
    }

    public class ApplicationCommandsProxy : IApplicationCommands
    {
        public CompositeCommand ShowFlyoutCommand
        {
            get { return ApplicationCommands.ShowFlyoutCommand; }
        }
        public CompositeCommand NavigateToCommand
        {
            get { return ApplicationCommands.NavigateToCommand; }
        }
    }
}
