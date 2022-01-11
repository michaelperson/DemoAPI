using System.Windows.Input;

namespace WpfSignalR.Tools.Infrastructures.Interfaces
{
    public interface INavigateToService
    {
        ICommand NavigateToCommand { get; }

        bool CanNavigateTo(string ViewName);
        void NavigateTo(string ViewName);
    }
}