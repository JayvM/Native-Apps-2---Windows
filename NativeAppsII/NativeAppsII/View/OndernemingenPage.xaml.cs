using NativeAppsII.Model;
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
        private OndernemingenViewModel OndernemingviewModel ;
        private CategorieViewModel CategorieViewModel;
        private ObservableCollection<Onderneming> ondernemingen;
        private ObservableCollection<Categorie> categorieën;

        private String naamFilter="";
        private String categorieFilter = "";
        private String gemeenteFilter = "";
        private bool isOpenFilter = false;



        public OndernemingenPage()
        {
            this.InitializeComponent();
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            OndernemingviewModel = new OndernemingenViewModel();
            CategorieViewModel = new CategorieViewModel();
           
            fillPage();

        }
        private async void fillPage()
        {
            ondernemingen = await OndernemingviewModel.getOndernemingen();
            lvOndernemingen.DataContext = ondernemingen;

            categorieën = await CategorieViewModel.getCategorieën();
            foreach(var categorie in categorieën)
            {
                TypeFilter.Items.Add(categorie.Naam);
            }
            List<String> gemeentes = ondernemingen.Select(on => on.Gemeente).ToList();
            foreach (var gemeente in gemeentes)
            {
                if(GemeenteFilter.Items.Where(e=>e.Equals(gemeente)).FirstOrDefault() == null)
                GemeenteFilter.Items.Add(gemeente);
            }
            
        }
        private async void filterLijst()
        {
            ondernemingen = await OndernemingviewModel.getOndernemingen();
            List<Onderneming> ondernemingenFiltered = ondernemingen.ToList();
            if (naamFilter.Equals("") && categorieFilter.Equals("") && gemeenteFilter.Equals("") && isOpenFilter == false)
            {
                lvOndernemingen.DataContext = ondernemingen;
            }
            else
            {
                if (!naamFilter.Equals(""))
                {
                    ondernemingenFiltered= ondernemingenFiltered.Where(onderneming => onderneming.Naam.ToLower().Contains(naamFilter)).ToList();
                }
                if(!categorieFilter.Equals(""))
                {
                    ondernemingenFiltered= ondernemingenFiltered.Where(onderneming => onderneming.Categorie.Naam.Equals(categorieFilter)).ToList();

                }
                if (!gemeenteFilter.Equals(""))
                {
                    ondernemingenFiltered = ondernemingenFiltered.Where(onderneming => onderneming.Gemeente.Equals(gemeenteFilter)).ToList();

                }
                if (isOpenFilter == true)
                {
                    ondernemingenFiltered = ondernemingenFiltered.Where(onderneming => onderneming.isOpen==true).ToList();

                }
                
                lvOndernemingen.DataContext = ondernemingenFiltered;
            }
        }

        private void GoToOnderneming(object sender, RoutedEventArgs e)
        {
            Onderneming onderneming = (sender as Button).DataContext as Onderneming;
            if (onderneming != null)
            {
                this.Frame.Navigate(typeof(OndernemingPage), onderneming);
            }
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            On_BackRequested();
        }

        // Handles system-level BackRequested events and page-level back button Click events
        private bool On_BackRequested()
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
                return true;
            }
            return false;
        }

        private void BackInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            On_BackRequested();
            args.Handled = true;
        }

        private void NaamFilter_Changed(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            naamFilter = NaamFilter.Text.ToLower();
            filterLijst();
        }

        private void TypeFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TypeFilter.SelectedItem != null)
            {
                categorieFilter = TypeFilter.SelectedValue.ToString();
                filterLijst();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            categorieFilter = "";
            naamFilter = "";
            NaamFilter.Text = "";
            TypeFilter.SelectedItem = null;
            gemeenteFilter = "";
            GemeenteFilter.SelectedItem = null;
            isOpenFilter = false;
            filterLijst();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GemeenteFilter.SelectedItem != null)
            {
                gemeenteFilter = GemeenteFilter.SelectedValue.ToString();
                filterLijst();
            }
        }

        private void IsOpenFilter(object sender, RoutedEventArgs e)
        {
            isOpenFilter = true;
            filterLijst();
        }

        
    }
}

