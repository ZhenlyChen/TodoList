﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Windows;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Core;
using Windows.UI.Xaml.Media.Animation;

namespace MyList {
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page {
        public static MainPage Current;
        public DataItems ListItemsData { get; set; }
        public MainPage() {
            this.InitializeComponent();
            ListItemsData = new DataItems();
            Current = this;
            ListFrame.Navigate(typeof(ListPage));
            NewFrame.Navigate(typeof(NewPage));
            this.SizeChanged += (s, e) => {
                if (ListFrame.Visibility == Visibility.Visible) {
                    SetRightAuto();
                }
                if (e.NewSize.Width > 800) {
                    ListFrame.Visibility = Visibility.Visible;
                    SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                        AppViewBackButtonVisibility.Collapsed;
                    MainPageGrid.ColumnDefinitions[1].Width = new GridLength(400);
                }
            };
            SystemNavigationManager.GetForCurrentView().BackRequested += (s, e) => {
                GoBackPage();
            };
            SetRightAuto();
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Background.jpg", UriKind.Absolute));
            MainPageGrid.Background = imageBrush;
        }

        private bool IsSmallScreen() {
            return Window.Current.Bounds.Width <= 800;
        }

        private void SetLeftAuto() {
            MainPageGrid.ColumnDefinitions[0].Width = GridLength.Auto;
            MainPageGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
        }

        private void SetRightAuto() {
            MainPageGrid.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
            MainPageGrid.ColumnDefinitions[1].Width = GridLength.Auto;
        }

        public void GoBackPage() {
            ListFrame.Visibility = Visibility.Visible;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                AppViewBackButtonVisibility.Collapsed;
            SetRightAuto();
            if (IsSmallScreen()) {
                NewFrame.Visibility = Visibility.Collapsed;
            }
        }

        public void GoToNewPage() {
            if (IsSmallScreen()) {
                //ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("image", ListFrame);
                //ConnectedAnimation imageAnimation =
                //    ConnectedAnimationService.GetForCurrentView().GetAnimation("image");
                //if (imageAnimation != null) {
                //    imageAnimation.TryStart(NewFrame);
                //}
                ListFrame.Visibility = Visibility.Collapsed;
                NewFrame.Visibility = Visibility.Visible;
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Visible;
                SetLeftAuto();
            }
        }

        private void Button_GotoNewPage(object sender, RoutedEventArgs e) {
            NewPage.Current.IsCreateStatus = true;
            ListPage.Current.ItemSelected = -1;
            GoToNewPage();
        }

        private void Button_DeleteItem(object sender, RoutedEventArgs e) {
            if (ListPage.Current.ItemSelected != -1) {
                ListItemsData.TodoLists.RemoveAt(ListPage.Current.ItemSelected);
                MainPage.Current.GoBackPage();
            }
        }

        private void Background_Click_1(object sender, RoutedEventArgs e) {
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Background.jpg", UriKind.Absolute));
            MainPageGrid.Background = imageBrush;
        }
        private void Background_Click_2(object sender, RoutedEventArgs e) {
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Background2.jpg", UriKind.Absolute));
            MainPageGrid.Background = imageBrush;
        }
        private void Background_Click_3(object sender, RoutedEventArgs e) {
            ImageBrush imageBrush = new ImageBrush();
            MainPageGrid.Background = imageBrush;
        }
    }


}
