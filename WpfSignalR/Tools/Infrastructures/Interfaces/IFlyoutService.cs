using System.Windows.Input;

namespace WpfSignalR.Tools.Infrastructures.Interfaces
{
    public interface IFlyoutService
    {
        ICommand ShowFlyoutCommand { get; }

        bool CanShowFlyout(string flyoutName);
        void ShowFlyout(string flyoutName);
    }
}