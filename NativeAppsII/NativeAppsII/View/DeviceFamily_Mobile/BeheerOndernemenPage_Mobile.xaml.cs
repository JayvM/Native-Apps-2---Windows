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

namespace NativeAppsII.View.DeviceFamily_Mobile
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BeheerOndernemenPage_Mobile : Page
    {
        Onderneming huidigeOnderneming;
        Actie huidigeActie;
        Evenement huidigEvenement;
        OndernemingenViewModel ondernemingViewModel;
        ActieViewModel actieViewModel;
        EvenementViewModel evenementViewModel;
        public BeheerOndernemenPage_Mobile()
        {
            ondernemingViewModel = new OndernemingenViewModel();
            actieViewModel = new ActieViewModel();
            evenementViewModel = new EvenementViewModel();

            this.InitializeComponent();
            Initialize();
        }

        public async void Initialize()
        {
            ObservableCollection<Onderneming> ondernemingen = await ondernemingViewModel.getOndernemingenVanGebruiker(((App)Application.Current).gebruiker.Id);
            Name.Text = ((App)Application.Current).gebruiker.Gebruikersnaam;
            Ondernemingen.DataContext = ondernemingen;
            ActieT.Visibility = Visibility.Collapsed;
            EvenementT.Visibility = Visibility.Collapsed;

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
                    ActieT.Visibility = Visibility.Visible;
                    EvenementT.Visibility = Visibility.Visible;
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
            this.Frame.Navigate(typeof(OndernemingToevoegenPage_Mobile));
        }

        private void ActieToevoegen(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ActieToevoegenPage_Mobile), huidigeOnderneming.Id.ToString());
        }

        private void EvenementToevoegen(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EvenementToevoegenPage_Mobile), huidigeOnderneming.Id.ToString());
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

                    huidigEvenement = evenementen.SelectedValue as Evenement;
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
            Frame.Navigate(typeof(OndernemingToevoegenPage_Mobile), huidigeOnderneming);

        }
        private void SaveAsyncA(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ActieToevoegenPage_Mobile), huidigeActie);
        }
        private void SaveAsyncE(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EvenementToevoegenPage_Mobile), huidigEvenement);

        }

        private async void RemoveO(object sender, RoutedEventArgs e)
        {
            await ondernemingViewModel.deleteOnderneming(huidigeOnderneming.Id);
            ObservableCollection<Onderneming> ondernemingen = await ondernemingViewModel.getOndernemingenVanGebruiker(((App)Application.Current).gebruiker.Id);
            Ondernemingen.DataContext = ondernemingen;

            // Frame.Navigate(typeof(BeheerOndernemerPage), huidigeOnderneming);


        }
        private async void RemoveA(object sender, RoutedEventArgs e)
        {
            await actieViewModel.deleteActie(huidigeActie.Id);
            ObservableCollection<Onderneming> ondernemingen = await ondernemingViewModel.getOndernemingenVanGebruiker(((App)Application.Current).gebruiker.Id);
            huidigeOnderneming = ondernemingen.FirstOrDefault(a => a.Id == huidigeOnderneming.Id);
            acties.DataContext = huidigeOnderneming.Acties;



        }
        private async void RemoveE(object sender, RoutedEventArgs e)
        {
            await evenementViewModel.deleteEvenement(huidigEvenement.Id);
            ObservableCollection<Onderneming> ondernemingen = await ondernemingViewModel.getOndernemingenVanGebruiker(((App)Application.Current).gebruiker.Id);
            huidigeOnderneming = ondernemingen.FirstOrDefault(a => a.Id == huidigeOnderneming.Id);
            evenementen.DataContext = huidigeOnderneming.Evenementen;


        }

        private void Panel(object sender, RoutedEventArgs e)
        {
            if (splitView.IsPaneOpen)
            {
                splitView.IsPaneOpen = false;
            }
            else
            {
                splitView.IsPaneOpen = true;
            }
        }
    }
}
