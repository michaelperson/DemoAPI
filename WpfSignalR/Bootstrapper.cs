using Prism.Ioc;
using Prism.Unity;
using System.Windows;

namespace WpfSignalR
{
    public class Bootstrapper : PrismBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Window>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
             
        }

        protected override void InitializeShell(DependencyObject shell)
        {
            base.InitializeShell(shell); 

            Application.Current.MainWindow = (Window)this.Shell;
            Application.Current.MainWindow.Show();
        }
    }
}
