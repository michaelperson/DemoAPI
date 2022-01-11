using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfSignalR.Tools.Infrastructures.Constants;
using WpfSignalR.Tools.Infrastructures.Interfaces;

namespace WpfSignalR.Tools.Infrastructures.Services
{
    public class NavigateService : INavigateToService
    {
        IRegionManager _regionManager;

        public ICommand NavigateToCommand { get; private set; }
        public NavigateService() { }
        public NavigateService(IRegionManager regionManager, IApplicationCommands applicationCommands)
        {
            _regionManager = regionManager;

            NavigateToCommand = new DelegateCommand<string>(NavigateTo, CanNavigateTo);
            applicationCommands.NavigateToCommand.RegisterCommand(NavigateToCommand);
        }

        public bool CanNavigateTo(string ViewName)
        {
             return true;  
        }

        public void NavigateTo(string ViewName)
        {
            IRegion region = _regionManager.Regions[RegionNames.MainRegion];

            if (region != null)
            {
                region.RemoveAll();
                region.NavigationService.RequestNavigate(
                                 new Uri(ViewName, UriKind.Relative), (result) => { 
                                     Console.WriteLine(result); 
                                 });
            }
        }
    }
}
