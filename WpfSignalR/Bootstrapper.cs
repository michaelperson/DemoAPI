using CommonServiceLocator;
using Microsoft.Extensions.DependencyInjection;
using Prism.Events;
using Prism.Ioc;
using Prism.Regions;
using Prism.Unity;
using System;
using System.ComponentModel.Design;
using System.Windows;
using Unity;
using Unity.Lifetime;
using Unity.Resolution;
using WpfSignalR.Tools.Infrastructures;
using WpfSignalR.Tools.Infrastructures.Constants;
using WpfSignalR.Tools.Infrastructures.Interfaces;
using WpfSignalR.Tools.Infrastructures.Services;
using WpfSignalR.Views;
using WpfSignalR.Views.Region;
using WpfSignalR.Views.Region.Flyout;

namespace WpfSignalR
{
    public class Bootstrapper : PrismBootstrapper
    {
        public static ServiceProvider serviceProvider;

       
        protected override DependencyObject CreateShell()
        {

            
            return Container.Resolve<MainWindow>();

        }

        private void ConfigureContainer()
        {
            Container.GetContainer().RegisterType<INavigateToService, NavigateService>();
            // Application commands
            Container.GetContainer().RegisterType<IApplicationCommands, ApplicationCommandsProxy>();
            //// Navigation service 
            Container.GetContainer().RegisterInstance(typeof(INavigateToService), "NavigateService", Container.Resolve<NavigateService>(), InstanceLifetime.Singleton);
            //// Flyout service
            Container.GetContainer().RegisterInstance(typeof(FlyoutService), "FlyoutService", Container.Resolve<FlyoutService>(), InstanceLifetime.Singleton);
            ////IApiRequesterService
             
            Container.GetContainer().RegisterInstance(typeof(IApiRequesterService), "ApiRequesterService", Container.Resolve<ApiRequesterService>(), InstanceLifetime.Singleton);


        }

        private void ConfigureServices(ServiceCollection services)
        {


            services.AddSingleton<IEventAggregator, EventAggregator>();
            services.AddSingleton<IContainerProvider>((m) => this.Container); 
            services.AddSingleton<IMetroMessageDisplayService, MetroMessageDisplayService>();
            services.AddSingleton<IRegionManager>((m) => this.Container.Resolve<IRegionManager>());

        }





        protected override void InitializeShell(DependencyObject shell)
        {
            base.InitializeShell(shell);
   
            // Register views
            IRegionManager regionManager = serviceProvider.GetRequiredService<IRegionManager>();
            if (regionManager != null)
            {
                // Add right windows commands
                regionManager.RegisterViewWithRegion(RegionNames.RightWindowCommandsRegion, typeof(RightTitlebarCommands));
                //// Add flyouts
                regionManager.RegisterViewWithRegion(RegionNames.FlyoutRegion, typeof(ProfilFlyOut));
                regionManager.RegisterViewWithRegion(RegionNames.FlyoutRegion, typeof(ShellSettingsFlyout));
                // Add LOgin to MainRegion                
                regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(Login));
                
            }


            Application.Current.MainWindow = (MainWindow)shell;
            Application.Current.MainWindow.Show(); 
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
           
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();

            ConfigureNavigation(containerRegistry);
            ConfigureContainer();
        }

        private void ConfigureNavigation(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation(typeof(HomeTiles), UCNames.Home);
            containerRegistry.RegisterForNavigation(typeof(Login), UCNames.Login);
        }
    }
}
