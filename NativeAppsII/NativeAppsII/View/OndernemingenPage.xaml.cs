﻿using NativeAppsII.Model;
using NativeAppsII.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NativeAppsII.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OndernemingenPage : Page
    {
        private OndernemingenViewModel viewModel ;
        private ObservableCollection<Onderneming> companies;

        public OndernemingenPage()
        {
            this.InitializeComponent();
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            viewModel = new OndernemingenViewModel();
            filterLijstAsync();

        }
        private async void filterLijstAsync()
        {

            companies = await viewModel.getOndernemingen();

            lvOndernemingen.DataContext = companies;
        }

        private void LvOndernemingen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}