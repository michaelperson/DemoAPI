using Prism.Ioc;
using Prism.Unity;
using System.Windows;
using WpfSignalR.Views;

namespace WpfSignalR
{
    public class Bootstrapper : PrismBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
             
        }

        protected override void InitializeShell(DependencyObject shell)
        {
            base.InitializeShell(shell); 

            Application.Current.MainWindow = (MainWindow)this.Shell;
            Application.Current.MainWindow.Show();
        }
    }
}
