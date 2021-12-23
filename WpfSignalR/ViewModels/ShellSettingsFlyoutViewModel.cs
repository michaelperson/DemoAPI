using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfSignalR.Tools.Infrastructures.Base;
using MahApps.Metro;
using WpfSignalR.Models.visual;
using System.Windows;
using WpfSignalR.Tools.Infrastructures.Events;
using System.Globalization;
using ControlzEx.Theming;
using System.Windows.Media;

namespace WpfSignalR.ViewModels
{
    public class ShellSettingsFlyoutViewModel : ViewModelBase
    {
        

        #region CTOR

        /// <summary>
        /// CTOR
        /// </summary>
        public ShellSettingsFlyoutViewModel()
        {
            
            // create metro theme color menu items for the demo
            this.ApplicationThemes = ThemeManager.Current.Themes
                                           .Select(a => new ApplicationTheme() { Name = a.Name, BorderColorBrush = a.Resources["BlackColorBrush"] as Brush, ColorBrush = a.Resources["WhiteColorBrush"] as Brush })
                                           .ToList();

            // create accent colors list
            this.AccentColors = ThemeManager.Current.ColorSchemes
                                            .Select(a => new AccentColor() { Name = "Cyan", ColorBrush= Brushes.Cyan })
                                            .ToList();

            this.SelectedTheme = this.ApplicationThemes.FirstOrDefault();
            this.SelectedAccentColor = this.AccentColors.Where(c => c.Name.Equals("Cyan")).FirstOrDefault();
        }

        #endregion CTOR

        #region Properties

        private IList<ApplicationTheme> applicationsThemes;

        /// <summary>
        /// List with application themes
        /// </summary>
        public IList<ApplicationTheme> ApplicationThemes
        {
            get { return applicationsThemes; }
            set { this.SetProperty<IList<ApplicationTheme>>(ref this.applicationsThemes, value); }
        }

        private IList<AccentColor> accentColors;

        /// <summary>
        /// List with accent colors
        /// </summary>
        public IList<AccentColor> AccentColors
        {
            get { return accentColors; }
            set { this.SetProperty<IList<AccentColor>>(ref this.accentColors, value); }
        }

        private ApplicationTheme selectedTheme;

        /// <summary>
        /// The selected theme
        /// </summary>
        public ApplicationTheme SelectedTheme
        {
            get { return selectedTheme; }
            set
            {
                if (this.SetProperty<ApplicationTheme>(ref this.selectedTheme, value))
                {
                    var theme = ThemeManager.Current.DetectTheme(Application.Current);
                    var appTheme = ThemeManager.Current.GetTheme(value.Name);
                    ThemeManager.Current.ChangeTheme(Application.Current, appTheme);

                    EventAggregator.GetEvent<StatusBarMessageUpdateEvent>().Publish(String.Format("Theme changed to {0}", value.Name));
                }
            }
        }

        private AccentColor selectedAccentColor;

        /// <summary>
        /// Selected accent color
        /// </summary>
        public AccentColor SelectedAccentColor
        {
            get { return selectedAccentColor; }
            set
            {
                if (this.SetProperty<AccentColor>(ref this.selectedAccentColor, value))
                {
                    var theme = ThemeManager.Current.DetectTheme(Application.Current);
                    var accent = ThemeManager.Current.GetTheme(value.Name);
                    //ThemeManager.ChangeAppStyle(Application.Current, accent, theme.Item1);

                    EventAggregator.GetEvent<StatusBarMessageUpdateEvent>().Publish(String.Format("Accent color changed to {0}", value.Name));
                }
            }
        }


        
        

        #endregion Properties
    }
}
