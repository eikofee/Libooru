﻿using Libooru.Links;
using Libooru.Views;
using MetroRadiance.UI;
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

namespace Libooru
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Views.MainPage mainPage { get; set; }

        private Views.MenuPage menuPage { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            var core = new Core(this);
            ThemeService.Current.ChangeTheme(Theme.Dark);
            Switcher.pageSwitcher = this;
            mainPage = new Views.MainPage();
            menuPage = new Views.MenuPage();
            mainPage.core = core;
            menuPage.core = core;
            Switcher.Switch(mainPage);
            mainPage.UpdateView();
        }

        public void GoToMain()
        {
            Switcher.Switch(mainPage);
            mainPage.UpdateView();
        }

        public void GoToMenu()
        {
            Switcher.Switch(menuPage);
            menuPage.UpdateView();
        }

        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }

        public void Navigate(UserControl nextPage, Core core)
        {
            this.Content = nextPage;
            ISwitchable s = nextPage as ISwitchable;

            if (s != null)
                s.UtilizeState(core);
            else
                throw new ArgumentException("NextPage is not ISwitchable! "
                  + nextPage.Name.ToString());
        }

        public void Navigate(Page nextPage)
        {
            this.Content = nextPage;
        }

        public void Navigate(Page nextPage, Core core)
        {
            this.Content = nextPage;
            ISwitchable s = nextPage as ISwitchable;

            if (s != null)
                s.UtilizeState(core);
            else
                throw new ArgumentException("NextPage is not ISwitchable! "
                  + nextPage.Name.ToString());
        }

    }
}