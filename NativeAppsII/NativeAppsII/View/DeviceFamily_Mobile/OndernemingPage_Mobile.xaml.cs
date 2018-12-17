using NativeAppsII.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NativeAppsII.View.DeviceFamily_Mobile
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OndernemingPage_Mobile : Page
    {
        public OndernemingPage_Mobile()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Onderneming onderneming = e.Parameter as Onderneming;
            this.DataContext = onderneming;
            lvActies.DataContext = onderneming.Acties;
            lvEvenementen.DataContext = onderneming.Evenementen;
            MapControl.MapServiceToken = "W92mdgL8YZQAXPAWgrxL~QS6p5UkRoDCGTtVDWe0grQ~Av_PduDzslv9yMaRivxJoNZ0HEp7TJORFm3LIz-I6-2yCgO1AjgaXW-RyWYLf9FF ";
            getLocation(onderneming);
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

        private async void getLocation(Onderneming onderneming)
        {
            BasicGeoposition queryHint = new BasicGeoposition();
            queryHint.Latitude = 50.8503;
            queryHint.Longitude = 4.3517;
            Geopoint hintPoint = new Geopoint(queryHint);

            MapLocationFinderResult result = await MapLocationFinder.FindLocationsAsync(onderneming.Adres, hintPoint, 1);

            // If the query returns results, display the coordinates
            // of the first result.
            if (result.Status == MapLocationFinderStatus.Success)
            {
                var MyLandmarks = new List<MapElement>();
                BasicGeoposition snPosition = new BasicGeoposition { Latitude = result.Locations[0].Point.Position.Latitude, Longitude = result.Locations[0].Point.Position.Longitude };
                Geopoint snPoint = new Geopoint(snPosition);
                var ondernemingLocatie = new MapIcon
                {
                    Location = snPoint,
                    NormalizedAnchorPoint = new Point(0.5, 1.0),
                    ZIndex = 0,
                    Title = onderneming.Naam
                };
                MyLandmarks.Add(ondernemingLocatie);

                var LandmarksLayer = new MapElementsLayer
                {
                    ZIndex = 1,
                    MapElements = MyLandmarks
                };

                MapControl.Layers.Add(LandmarksLayer);

                MapControl.Center = snPoint;
                MapControl.ZoomLevel = 14;


            }
        }

        private void openActie(object sender, RoutedEventArgs e)
        {
            Actie actie = (sender as Button).DataContext as Actie;
            ShowDialog(actie);

        }
        private async void ShowDialog(Actie actie)
        {
            var dialog = new ContentDialog()
            {
                Title = actie.Beschrijving,
                FullSizeDesired = true,
                MaxWidth = this.ActualWidth // Required for Mobile!
            };

            var panel = new StackPanel();

            panel.Children.Add(new TextBlock
            {
                Text = "Geldig tot: " + actie.GeldigTot,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 0, 0, 10)
            });
            Image image = new Image();
            BitmapImage imageBitMap = new BitmapImage(new Uri(this.BaseUri, "/Assets/QRcode.jpg"));
            ImageSource source = imageBitMap;
            image.Source = source;
            panel.Children.Add(image);

            dialog.Content = panel;

            dialog.CloseButtonText = "Close";

            var result = await dialog.ShowAsync();
        }

        private async void addEvenement(object sender, RoutedEventArgs e)
        {
            Evenement evenement = (sender as Button).DataContext as Evenement;
            Onderneming onderneming = DataContext as Onderneming;
            // Create an Appointment that should be added the user's appointments provider app.
            var appointment = new Windows.ApplicationModel.Appointments.Appointment();
            //Locatie
            appointment.Location = evenement.Plaats;
            //Naam
            appointment.Subject = "Evenement van " + onderneming.Naam;
            //Details
            appointment.Details = evenement.Beschrijving;

            appointment.StartTime = evenement.Datum;

            var rect = new Rect();
            // ShowAddAppointmentAsync returns an appointment id if the appointment given was added to the user's calendar.
            // This value should be stored in app data and roamed so that the appointment can be replaced or removed in the future.
            // An empty string return value indicates that the user canceled the operation before the appointment was added.
            String appointmentId = await Windows.ApplicationModel.Appointments.AppointmentManager.ShowAddAppointmentAsync(
                                   appointment, rect, Windows.UI.Popups.Placement.Default);


        }

    }
}
