using NativeAppsII.Model;
using NativeAppsII.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using XmlDocument = Windows.Data.Xml.Dom.XmlDocument;
using XmlElement = Windows.Data.Xml.Dom.XmlElement;
using XmlNodeList = Windows.Data.Xml.Dom.XmlNodeList;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NativeAppsII.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ActieToevoegenPage : Page
    {

        ActieViewModel actieViewModel;
        Actie actieObject;
        String bedrijf;

        public ActieToevoegenPage()
        {
            this.InitializeComponent();
            actieViewModel = new ActieViewModel();
        }



        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {

            actieObject = e.Parameter as Actie;
            bedrijf = e.Parameter as String;
            if (actieObject != null)
            {
                Beschrijving.Text = actieObject.Beschrijving;
                Datum.Date = actieObject.getDate;
                Time.Time = actieObject.getHour;


            }
        }
        private async void SaveAsync(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var actieAnswer = false;
            if (Validate())
            {

                    if (actieObject != null)
                    {
                        actieObject.Beschrijving = Beschrijving.Text;
                        var dateObject = Datum.Date.Date.ToString().Substring(0, 10) + " " + Time.Time.ToString();
                        DateTime myDateObject = DateTime.Parse(dateObject);
                        actieObject.GeldigTot = myDateObject;
                        actieAnswer = await actieViewModel.bewerkActieAsync(actieObject);
                        if (actieAnswer)
                        {
                            this.Frame.Navigate(typeof(BeheerOndernemerPage));
                            ToastTemplateType toastTemplate = ToastTemplateType.ToastText02;
                            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                            XmlNodeList toastTekstElementen = toastXml.GetElementsByTagName("text");
                            toastTekstElementen[0].AppendChild(toastXml.CreateTextNode("Actie"));
                            toastTekstElementen[1].AppendChild(toastXml.CreateTextNode(actieObject.Beschrijving + " aangepast"));
                            IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
                            ((XmlElement)toastNode).SetAttribute("duration", "long");
                            ToastNotification toast = new ToastNotification(toastXml);
                            ToastNotificationManager.CreateToastNotifier().Show(toast);
                        }
                    }

                    else
                    {
                        var date = Datum.Date.Date.ToString().Substring(0, 10) + " " + Time.Time.ToString();
                        DateTime myDate = DateTime.Parse(date);
                        Actie actie = new Actie(
                            Beschrijving.Text,
                            myDate, Int32.Parse(bedrijf)
                           

                            );
                        actieAnswer = await actieViewModel.addActieAsync(actie);
                        if (actieAnswer)
                        {
                            this.Frame.Navigate(typeof(BeheerOndernemerPage));
                            ToastTemplateType toastTemplate = ToastTemplateType.ToastText02;
                            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                            XmlNodeList toastTekstElementen = toastXml.GetElementsByTagName("text");
                            toastTekstElementen[0].AppendChild(toastXml.CreateTextNode("Actie"));
                            toastTekstElementen[1].AppendChild(toastXml.CreateTextNode(actie.Beschrijving + " werd toegevoegd aan je acties"));
                            IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
                            ((XmlElement)toastNode).SetAttribute("duration", "long");
                            ToastNotification toast = new ToastNotification(toastXml);
                            ToastNotificationManager.CreateToastNotifier().Show(toast);
                        }
                    }
                    
                
            }
        }

    
        

        private bool Validate()
        {
            var validation = true;
            if (Beschrijving.Text.Trim() == "")
            {
                beschrijvingError.Visibility = Visibility.Visible;
                validation = false;
            }
            else
            {
                beschrijvingError.Visibility = Visibility.Collapsed;

            }
            return validation;
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


    }
}

