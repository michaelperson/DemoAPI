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
using WpfSignalR.Tools.Infrastructures.Interfaces;

namespace WpfSignalR.Views.Region.Flyout
{
    /// <summary>
    /// Interaction logic for ProfilFlyOut.xaml
    /// </summary>
    public partial class ProfilFlyOut : IFlyoutView
    {
        public ProfilFlyOut()
        {
            InitializeComponent();
        }

        public string FlyoutName => "ProfilFlyout";
    }
}
