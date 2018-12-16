using NativeAppsII.Model;
using NativeAppsII.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NativeAppsII.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegistreerPage : Page
    {
        GebruikerViewModel gebruikerViewModel;
        public RegistreerPage()
        {
            gebruikerViewModel = new GebruikerViewModel();
            this.InitializeComponent();
            Soort.Items.Add("Ondernemer");
            Soort.Items.Add("Gebruiker");
        }
        private async void SaveAsync(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (Validate())
            {
                Gebruiker gebruiker = new Gebruiker(Gebruikersnaam.Text, Password.Password.ToString(), Soort.SelectedItem.ToString());
                var answer = await gebruikerViewModel.Register(gebruiker);
                if(answer == "error")
                {
                    usernameError.Text = "Gebruikersnaam in gebruik";
                    usernameError.Visibility = Visibility.Visible;
                }
                else
                { 
                    string[] tokens = answer.Split('"');
                    var token = tokens[4];
                    Gebruiker user = new Gebruiker(Int32.Parse(tokens[21].ToString()),tokens[13].ToString(), tokens[17].ToString());
                    ((App)Application.Current).gebruiker = user;
                    GlobalToken.Token = tokens[3].ToString();
                    this.Frame.Navigate(typeof(OndernemingenPage));
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTekstElementen = toastXml.GetElementsByTagName("text");
                    toastTekstElementen[0].AppendChild(toastXml.CreateTextNode("Welkom " + user.Gebruikersnaam + " :)"));
                    IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
                    ((XmlElement)toastNode).SetAttribute("duration", "long");
                    ToastNotification toast = new ToastNotification(toastXml);
                    ToastNotificationManager.CreateToastNotifier().Show(toast);
                }
            }


        }
        private bool Validate()
        {
            var validation = true;
            if (Soort.SelectedItem == null)
            {
                soortError.Visibility = Visibility.Visible;
                validation = false;
            }
            else
            {
                usernameError.Text = "Vul gebruikersnaam in";
                soortError.Visibility = Visibility.Collapsed;

            }
            if (Gebruikersnaam.Text == "")
            {

                usernameError.Visibility = Visibility.Visible;
                validation = false;
            }
            else
            {
                usernameError.Visibility = Visibility.Collapsed;

            }
            if (Password.Password.ToString() == "")
            {
                wachtwoordError.Visibility = Visibility.Visible;
                validation = false;
            }
            else
            {
                wachtwoordError.Visibility = Visibility.Collapsed;

            }
            if (validatePassword.Password.ToString() == "")
            {
                validatewachtwoordError.Visibility = Visibility.Visible;
                validation = false;
            }
            else
            {
                if (Password.Password.ToString() != validatePassword.Password.ToString() && validatePassword.Password.ToString() != "")
                {
                    validatewachtwoordError.Text = "Wachtwoord en validate moeten gelijk zijn";
                    validatewachtwoordError.Visibility = Visibility.Visible;
                    validation = false;
                }
                else
                {
                    validatewachtwoordError.Visibility = Visibility.Collapsed;

                }

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
