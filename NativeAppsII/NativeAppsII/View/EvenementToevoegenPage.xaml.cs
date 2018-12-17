using NativeAppsII.Model;
using NativeAppsII.ViewModel;
using System;
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
using XmlDocument = Windows.Data.Xml.Dom.XmlDocument;
using XmlNodeList = Windows.Data.Xml.Dom.XmlNodeList;
using XmlElement = Windows.Data.Xml.Dom.XmlElement;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NativeAppsII.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EvenementToevoegenPage : Page
    {
        EvenementViewModel evenementViewModel;
        OndernemingenViewModel ondernemingenViewModel;
        Evenement evenementObject;

        public EvenementToevoegenPage()
        {
            this.InitializeComponent();
            evenementViewModel = new EvenementViewModel();
            ondernemingenViewModel = new OndernemingenViewModel();
        }



        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
           
             evenementObject = e.Parameter as Evenement;
            if (evenementObject != null)
            {
                Beschrijving.Text = evenementObject.Beschrijving;
                Datum.Date = evenementObject.getDate;
                Time.Time = evenementObject.getHour;
                Plaats.Text = evenementObject.Plaats;


            }
            else
            {
                this.DataContext = null;
                
            }


        }
        private async void SaveAsync(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (Validate())
            {
                if (evenementObject != null)
                {
                    var dateObject = Datum.Date.Date.ToString().Substring(0, 10) + " " + Time.Time.ToString();
                    DateTime myDateObject = DateTime.Parse(dateObject);
                    evenementObject.Beschrijving = Beschrijving.Text;
                    evenementObject.Plaats = Plaats.Text;
                    evenementObject.Datum = myDateObject;

                     var evenementAnswer = await evenementViewModel.bewerkEvenementAsync(evenementObject);
                    if (evenementAnswer)
                    {
                        this.Frame.Navigate(typeof(BeheerOndernemerPage));
                        ToastTemplateType toastTemplate = ToastTemplateType.ToastText02;
                        XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                        XmlNodeList toastTekstElementen = toastXml.GetElementsByTagName("text");
                        toastTekstElementen[0].AppendChild(toastXml.CreateTextNode("Evenement"));
                        toastTekstElementen[1].AppendChild(toastXml.CreateTextNode(evenementObject.Beschrijving + " aangepast"));
                        IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
                        ((XmlElement)toastNode).SetAttribute("duration", "long");
                        ToastNotification toast = new ToastNotification(toastXml);
                        ToastNotificationManager.CreateToastNotifier().Show(toast);
                    }
                    else
                    {
                        var date = Datum.Date.Date.ToString().Substring(0, 10) + " " + Time.Time.ToString();
                        DateTime myDate = DateTime.Parse(date);
                        Evenement evenement = new Evenement(
                            Beschrijving.Text,
                            Plaats.Text,
                            myDate, 1
                            );
                        var evenementAnswerObject = await evenementViewModel.addEvenementAsync(evenement);
                        if (evenementAnswerObject)
                        {
                            this.Frame.Navigate(typeof(BeheerOndernemerPage));
                            ToastTemplateType toastTemplate = ToastTemplateType.ToastText02;
                            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                            XmlNodeList toastTekstElementen = toastXml.GetElementsByTagName("text");
                            toastTekstElementen[0].AppendChild(toastXml.CreateTextNode("Evenement"));
                            toastTekstElementen[1].AppendChild(toastXml.CreateTextNode(evenement.Beschrijving + " werd toegevoegd aan je evenementen"));
                            IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
                            ((XmlElement)toastNode).SetAttribute("duration", "long");
                            ToastNotification toast = new ToastNotification(toastXml);
                            ToastNotificationManager.CreateToastNotifier().Show(toast);
                        }
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

                if (Plaats.Text.Trim() == "")
                {
                    plaatsError.Visibility = Visibility.Visible;
                    validation = false;

                }
                else
                {
                    plaatsError.Visibility = Visibility.Collapsed;

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
