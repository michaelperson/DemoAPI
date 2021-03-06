using MahApps.Metro.Controls;
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

namespace WpfSignalR.Views.Region
{
    /// <summary>
    /// Interaction logic for ShellSettingsFlyout.xaml
    /// </summary>
    public partial class ShellSettingsFlyout : IFlyoutView
    {
        public ShellSettingsFlyout()
        {
            InitializeComponent();
        }

        public string FlyoutName => "ShellSettingsFlyout";
    }
}
