using NativeAppsII.Model;
using NativeAppsII.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Xml;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
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


namespace NativeAppsII.View.DeviceFamily_Mobile
{

    public sealed partial class OndernemingToevoegenPage_Mobile : Page
    {
        CategorieViewModel categorieViewModel;
        OndernemingenViewModel ondernemingenViewModel;
        Onderneming comp;

        private ObservableCollection<Categorie> categorieën;
        public OndernemingToevoegenPage_Mobile()
        {
            this.InitializeComponent();
            categorieViewModel = new CategorieViewModel();
            ondernemingenViewModel = new OndernemingenViewModel();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            categorieën = await categorieViewModel.getCategorieën();
            foreach (var categorie in categorieën)
            {
                Categorie.Items.Add(categorie.Naam);
            }

            comp = e.Parameter as Onderneming;
            if (comp != null)
            {
                Naam.Text = comp.Naam;
                Openingsuur.Text = comp.Openingsuur;
                Sluitsuur.Text = comp.Sluituur;
                Categorie.SelectedIndex = Categorie.Items.IndexOf(comp.Categorie.Naam);
                Gemeente.Text = comp.Gemeente;
                Straat.Text = comp.Straat;
                Land.Text = comp.Land;
                Website.Text = comp.Website;
                Telefoonnummer.Text = comp.Telefooonnummer;
                Beschrijving.Text = comp.Information;

                Categorie.SelectedItem = comp.Categorie;

            }

        }
        private async void SaveAsync(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (Validate())
            {
                if (comp != null)
                {
                    comp.Naam = Naam.Text;
                    comp.Openingsuur = Openingsuur.Text;
                    comp.Sluituur = Sluitsuur.Text;
                    comp.Categorie = await categorieViewModel.getCategorie(Categorie.SelectedValue.ToString());
                    comp.Gemeente = Gemeente.Text;
                    comp.Straat = Straat.Text;
                    comp.Land = Land.Text;
                    comp.Website = Website.Text;
                    comp.Telefooonnummer = Telefoonnummer.Text;
                    comp.Information = Beschrijving.Text;
                    var ondernemingAnswer = await ondernemingenViewModel.bewerkOnderneming(comp);
                    if (ondernemingAnswer)
                    {
                        this.Frame.Navigate(typeof(BeheerOndernemenPage_Mobile));
                        ToastTemplateType toastTemplate = ToastTemplateType.ToastText02;
                        XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                        XmlNodeList toastTekstElementen = toastXml.GetElementsByTagName("text");
                        toastTekstElementen[0].AppendChild(toastXml.CreateTextNode("Onderneming"));
                        toastTekstElementen[1].AppendChild(toastXml.CreateTextNode(comp.Naam + " werd aangepast"));
                        IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
                        ((XmlElement)toastNode).SetAttribute("duration", "long");
                        ToastNotification toast = new ToastNotification(toastXml);
                        ToastNotificationManager.CreateToastNotifier().Show(toast);
                    }
                }
                else
                {
                    var categorie = await categorieViewModel.getCategorie(Categorie.SelectedValue.ToString());
                    Onderneming onderneming = new Onderneming(
                        Naam.Text,
                        Openingsuur.Text,
                        Sluitsuur.Text,
                        categorie.Id,
                        Gemeente.Text,
                        Straat.Text,
                        Land.Text,
                        Website.Text,
                        Telefoonnummer.Text,
                        Beschrijving.Text,
                        ((App)Application.Current).gebruiker.Id
                        );
                    var ondernemingAnswer = await ondernemingenViewModel.addOnderneming(onderneming);
                    if (ondernemingAnswer)
                    {
                        this.Frame.Navigate(typeof(BeheerOndernemenPage_Mobile));
                        ToastTemplateType toastTemplate = ToastTemplateType.ToastText02;
                        XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                        XmlNodeList toastTekstElementen = toastXml.GetElementsByTagName("text");
                        toastTekstElementen[0].AppendChild(toastXml.CreateTextNode("Onderneming"));
                        toastTekstElementen[1].AppendChild(toastXml.CreateTextNode(onderneming.Naam + " werd toegevoegd aan je ondernemingen"));
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
            if (Naam.Text.Trim() == "")
            {
                naamError.Visibility = Visibility.Visible;
                validation = false;
            }
            else
            {
                naamError.Visibility = Visibility.Collapsed;

            }
            if (Categorie.SelectedItem == null)
            {
                categorieError.Visibility = Visibility.Visible;
                validation = false;

            }
            else
            {
                categorieError.Visibility = Visibility.Collapsed;

            }
            string pattern = "[0-9]{1,2}:[0-9]{1,2}";
            Match openingsuurMatch = Regex.Match(Openingsuur.Text, pattern);
            if (Openingsuur.Text.Trim() == "" || !openingsuurMatch.Success)
            {
                uurError.Visibility = Visibility.Visible;
                validation = false;

            }
            else
            {
                uurError.Visibility = Visibility.Collapsed;

            }
            Match sluituurMatch = Regex.Match(Sluitsuur.Text, pattern);
            if (Sluitsuur.Text.Trim() == "" || !sluituurMatch.Success)
            {
                uurError.Visibility = Visibility.Visible;
                validation = false;

            }
            else
            {
                uurError.Visibility = Visibility.Collapsed;

            }

            if (Gemeente.Text.Trim() == "")
            {
                gemeenteError.Visibility = Visibility.Visible;
                validation = false;

            }
            else
            {
                gemeenteError.Visibility = Visibility.Collapsed;

            }
            string straatpattern = "[A-Za-z]*[0-9]*";
            Match straatMatch = Regex.Match(Straat.Text, straatpattern);
            if (Straat.Text.Trim() == "" || !straatMatch.Success)
            {
                straatError.Visibility = Visibility.Visible;
                validation = false;

            }
            else
            {
                straatError.Visibility = Visibility.Collapsed;

            }
            if (Land.Text.Trim() == "")
            {
                landError.Visibility = Visibility.Visible;
                validation = false;

            }
            else
            {
                landError.Visibility = Visibility.Collapsed;

            }
            if (Website.Text.Trim() == "")
            {
                websiteError.Visibility = Visibility.Visible;
                validation = false;

            }
            else
            {
                websiteError.Visibility = Visibility.Collapsed;

            }
            string telefoonpattern = "[0-9]{9}";
            Match telefoonMatch = Regex.Match(Telefoonnummer.Text, telefoonpattern);
            if (Telefoonnummer.Text.Trim() == "" || !telefoonMatch.Success)
            {
                telefoonError.Visibility = Visibility.Visible;
                validation = false;

            }
            else
            {
                telefoonError.Visibility = Visibility.Collapsed;

            }
            if (Beschrijving.Text.Trim() == "")
            {
                beshcrijvingError.Visibility = Visibility.Visible;
                validation = false;

            }
            else
            {
                beshcrijvingError.Visibility = Visibility.Collapsed;

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
