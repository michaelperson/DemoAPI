using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using WpfSignalR.Tools.Infrastructures.Base;
using WpfSignalR.Tools.Infrastructures.Interfaces;
using WpfSignalR.Tools.Infrastructures.Security;
using WpfSignalR.Tools.Infrastructures.Services;

namespace WpfSignalR.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {


        private readonly INavigateToService _navigateToService;
        private readonly IApiRequesterService _apiRequesterService;

        private DelegateCommand _loginCmd;
        private string _login;
        private SecureString _password;

        public DelegateCommand LoginCmd { get => _loginCmd=_loginCmd??new DelegateCommand(LogMe, CanLogMe); }
        public string Login { get => _login; set { _login = value; LoginCmd.RaiseCanExecuteChanged(); } }
        public SecureString Password { get => _password; 
            set { 
                _password = value; LoginCmd.RaiseCanExecuteChanged(); 
            }
}

        public LoginViewModel(INavigateToService navigateToService, IApiRequesterService ApiRequesterService) : base()
        {
            _navigateToService = navigateToService;
            _apiRequesterService = ApiRequesterService;
        }

        private void LogMe()
        {
            string pass = SecurityTools.SecureStringToString(this.Password);

           bool rep = _apiRequesterService.Login(new Models.LoginModel() { Email = this.Login, Password = pass });
                 if(rep)
            {
                _navigateToService.NavigateToCommand.Execute("HomeTiles");
            }
                         
                     
;
            
        }

        private bool CanLogMe()
        {
            return (!string.IsNullOrWhiteSpace(Login) && Password != null) ;
        }
    }
}
