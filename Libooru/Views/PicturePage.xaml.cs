﻿using Libooru.Links;
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

namespace Libooru.Views
{
    /// <summary>
    /// Interaction logic for PicturePage.xaml
    /// </summary>
    public partial class PicturePage : Page, ISwitchable
    {

        public Core core { get; set; }

        public int DisplayedId { get; set; }

        public PicturePage()
        {
            InitializeComponent();
        }

        public void UtilizeState(Core core)
        {
            this.core = core;
        }

        public void UpdateView()
        {

        }

        private void goToMain(object sender, RoutedEventArgs e)
        {
            core.switcher.GoToMain();
        }

        public void LoadPicture(int id)
        {
            var p = core.picturesWroker.GetPicture(id);
            var b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri(p.Path);
            b.EndInit();

            this.image.Source = b;
            
        }
    }
}