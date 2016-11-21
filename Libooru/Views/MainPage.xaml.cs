﻿using MetroRadiance.UI;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Libooru.Links;
using System.Collections.ObjectModel;

namespace Libooru.Views
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page, ISwitchable
    {
        public Core core { get; set; }

        public ObservableCollection<Pic> listPic { get; set; }

        public MainPage()
        {
            InitializeComponent();
            ThemeService.Current.ChangeTheme(Theme.Dark);
            this.listPic = new ObservableCollection<Pic>();
            
        }

        public void UpdateView()
        {
            //RefreshList();
            CountFiles();
        }

        private void RefreshList(int index = 0, int limit = 5)
        {
            if (index >= listPic.Count)
            {
                var result = core.foldersWorker.getPictureFiles(index, limit);
                foreach (var item in result.list)
                {
                    listPic.Add(item);
                }
                this.picGrid.DataContext = this;
            }
        }

        public void CountFiles()
        {
            this.textInfos.Text = core.foldersWorker.pictureNumber + " picture"
                            + (core.foldersWorker.pictureNumber > 1 ? "s" : "")
                            + (core.foldersWorker.newPictureNumber > 0 ? " + "
                            + core.foldersWorker.newPictureNumber + " new" : "");
        }

        private void searchBar_GotFocus(object sender, RoutedEventArgs e)
        {
            this.searchBar.Text = "";
        }

        private void searchBar_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.searchBar.Text.Equals(""))
                this.searchBar.Text = "";
        }

        private void menuButton_Click(object sender, RoutedEventArgs e)
        {
            core.switcher.GoToMenu();
        }

        public void UtilizeState(Core core)
        {
            this.core = core;
        }

        private void mainlb_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.VerticalOffset + e.ViewportHeight >= e.ExtentHeight && e.VerticalOffset + e.ViewportHeight != 0)
            {
                RefreshList(listPic.Count);
            }
        }
    }
}
