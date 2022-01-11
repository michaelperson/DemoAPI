using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Unity;
using WpfSignalR.Tools.Infrastructures.Interfaces;
using WpfSignalR.ViewModels;

namespace WpfSignalR.Views.Region
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
   
    public partial class Login : UserControl, INavigationAware
    {
        private readonly INavigateToService _navigateToService;
        private readonly IApiRequesterService _apiRequesterService;
        [InjectionConstructor]
        public Login(INavigateToService navigateToService, IApiRequesterService ApiRequesterService)
        {
            InitializeComponent();
            this._navigateToService = navigateToService;
            this.DataContext = new LoginViewModel(navigateToService, ApiRequesterService);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((LoginViewModel)this.DataContext).Password = ((PasswordBox)sender).SecurePassword; }
        }

        #region INavigationAware Implementation
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            this.DataContext = new LoginViewModel(_navigateToService, _apiRequesterService);
        } 
        #endregion
    }
}
