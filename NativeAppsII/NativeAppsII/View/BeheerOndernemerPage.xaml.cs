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
    public sealed partial class BeheerOndernemerPage : Page
    {
        Onderneming huidigeOnderneming;
        Actie huidigeActie;
        Evenement huidigEvenement;
        OndernemingenViewModel ondernemingViewModel;
        public BeheerOndernemerPage()
        {
            ondernemingViewModel = new OndernemingenViewModel();
            this.InitializeComponent();
            Initialize();
        }

        public async void Initialize()
        {
            //var ondernemingen = await ondernemingViewModel.getOndernemingenVanGebruiker(((App)Application.Current).gebruiker.Id);
            ObservableCollection<Onderneming> ondernemingen = await ondernemingViewModel.getOndernemingenVanGebruiker(1);

          //  Name.Text = ((App)Application.Current).gebruiker.Gebruikersnaam;
            Ondernemingen.DataContext = ondernemingen;

        }
        
           private void NavLinksList_ItemClick(Object sender, Object args)
        {
            // Added to prevent errors when nothing was selected
            if (Ondernemingen.SelectedItems.Count > 0)
            {
                if (Ondernemingen.SelectedItems[0] != null)
                {
                    huidigeOnderneming = Ondernemingen.SelectedValue as Onderneming;
                    acties.DataContext = huidigeOnderneming.Acties;
                    evenementen.DataContext = huidigeOnderneming.Evenementen;
                    Naam.Text = huidigeOnderneming.Naam;
                    Openingsuur.Text = huidigeOnderneming.Openingsuur;
                    Sluitsuur.Text = huidigeOnderneming.Sluituur;
                    Gemeente.Text = huidigeOnderneming.Gemeente;
                    Categorie.Text = huidigeOnderneming.Categorie.Naam;
                    BeschrijvingO.Text = huidigeOnderneming.Information;
                    Website.Text = huidigeOnderneming.Website;
                    Straat.Text = huidigeOnderneming.Straat;
                    Telefoonnummer.Text = huidigeOnderneming.Telefooonnummer;
                    Land.Text = huidigeOnderneming.Land;
                    

                    Evenement.Visibility = Visibility.Collapsed;
                    Actie.Visibility = Visibility.Collapsed;
                    Onderneming.Visibility = Visibility.Visible;
                }
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

        private void OndernemingToevoegen(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(OndernemingToevoegenPage));
        }

        private void ActieToevoegen(object sender, RoutedEventArgs e)
        {
            
        }

        private void EvenementToeveogen(object sender, RoutedEventArgs e)
        {
            
        }


        private void Actie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (acties.SelectedItems.Count > 0)
            {
                if (acties.SelectedValue != null)
                {
                    huidigeActie = acties.SelectedValue as Actie;
                    Beschrijving.Text = huidigeActie.Beschrijving;
                    Datum.Date = huidigeActie.getDate;
                    Time.Time = huidigeActie.getHour;
                    evenementen.SelectedItem = null;
                    Evenement.Visibility = Visibility.Collapsed;
                    Actie.Visibility = Visibility.Visible;
                    Onderneming.Visibility = Visibility.Collapsed;

                }
            }
        }
        private void Evenement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (evenementen.SelectedItems.Count > 0)
            {
                if (evenementen.SelectedValue != null)
                {

                    huidigEvenement= evenementen.SelectedValue as Evenement;
                    BeschrijvingE.Text = huidigEvenement.Beschrijving;
                    DatumE.Date = huidigEvenement.getDate;
                    TimeE.Time = huidigEvenement.getHour;
                    Plaats.Text = huidigEvenement.Plaats;
                    Actie.Visibility = Visibility.Collapsed;
                    acties.SelectedItem = null;
                    Evenement.Visibility = Visibility.Visible;
                    Onderneming.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void SaveAsyncO(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(OndernemingToevoegenPage), huidigeOnderneming);

        }
        private void SaveAsyncA(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ActieToevoegenPage), huidigeActie);
        }
        private void SaveAsyncE(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EvenementToevoegenPage), huidigEvenement);

        }

        private void RemoveO(object sender, RoutedEventArgs e)
        {
 

        }
        private void RemoveA(object sender, RoutedEventArgs e)
        {
        

        }
        private void RemoveE(object sender, RoutedEventArgs e)
        {
            

        }
    }
}
