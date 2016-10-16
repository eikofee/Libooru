﻿using System;
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
using Libooru.Links;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Libooru.Views
{
    /// <summary>
    /// Interaction logic for MenuPage_Directories.xaml
    /// </summary>
    public partial class MenuPage_Directories : Page, ISwitchable
    {
        public Core core { get; set; }

        public MenuPage_Directories()
        {
            InitializeComponent();
            
        }

        public void UtilizeState(Core core)
        {
            this.core = core;
        }

        private void goToMenu(object sender, RoutedEventArgs e)
        {
            core.switcher.GoToMenu();
        }

        private void goToMain(object sender, RoutedEventArgs e)
        {
            core.switcher.GoToMain();
        }

        private void goToExternals(object sender, RoutedEventArgs e)
        {
            core.switcher.GoToMenu_Externals();
        }

        public void ApplyChanges(object sender, RoutedEventArgs e)
        {
            core.SetStatus("Applying changes...");
            core.config.Data.Folders.PictureFolderPath = textboxPictureFolder.Text;
            core.config.Data.Folders.NewPictureFolderPath = textboxNewPictureFolder.Text;
            core.config.ApplyChanges();
            core.SetStatus("Done.");
            UpdateView();
            core.SetStatus("");
        }

        public void ChooseDirectoryOpt1(object sender, RoutedEventArgs e)
        {
            ChooseDirectory(1);
        }

        public void ChooseDirectoryOpt2(object sender, RoutedEventArgs e)
        {
            ChooseDirectory(2);
        }

        public void ChooseDirectory(int opt)
        { 
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.Multiselect = false;
            string title = "";
            switch (opt)
            {
                case 1:
                    title = "Picture folder";
                    dialog.DefaultDirectory = core.config.Data.Folders.PictureFolderPath;
                    break;
                case 2:
                    title = "New pictures folder";
                    dialog.DefaultDirectory = core.config.Data.Folders.NewPictureFolderPath;
                    break;
            }
            dialog.Title = title;
            CommonFileDialogResult result = dialog.ShowDialog();
            switch(opt)
            {
                case 1:
                    textboxPictureFolder.Text = dialog.FileName;
                    break;
                case 2:
                    textboxNewPictureFolder.Text = dialog.FileName;
                    break;
            }
        }

        public void UpdateView()
        {
            CountFiles();
            SetPaths();
        }

        public void SetPaths()
        {
            textboxPictureFolder.Text = core.config.Data.Folders.PictureFolderPath;
            textboxNewPictureFolder.Text = core.config.Data.Folders.NewPictureFolderPath;
        }

        public void CountFiles()
        {
            this.textInfos.Text = core.foldersWorker.pictureNumber + " picture"
                            + (core.foldersWorker.pictureNumber > 1 ? "s" : "")
                            + (core.foldersWorker.newPictureNumber > 0 ? " + "
                            + core.foldersWorker.newPictureNumber + " new" : "");

        }
    }
}
