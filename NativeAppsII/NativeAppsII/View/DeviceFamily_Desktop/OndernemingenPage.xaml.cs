using NativeAppsII.Model;
using NativeAppsII.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Credentials;
using Windows.UI;
using Windows.UI.Core;
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
    public sealed partial class OndernemingenPage : Page
    {
        private OndernemingenViewModel OndernemingviewModel;
        private CategorieViewModel CategorieViewModel;
        private GebruikerViewModel gebruikerViewModel;
        private ObservableCollection<Onderneming> ondernemingen;
        private ObservableCollection<Categorie> categorieën;

        private String naamFilter = "";
        private String categorieFilter = "";
        private String gemeenteFilter = "";
        private bool isOpenFilter = false;



        public OndernemingenPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            OndernemingviewModel = new OndernemingenViewModel();
            CategorieViewModel = new CategorieViewModel();
            gebruikerViewModel = new GebruikerViewModel();
            fillPage();

        }
        private async void fillPage()
        {
            ondernemingen = await OndernemingviewModel.getOndernemingen();
            lvOndernemingen.DataContext = ondernemingen;

            categorieën = await CategorieViewModel.getCategorieën();
            foreach (var categorie in categorieën)
            {
                TypeFilter.Items.Add(categorie.Naam);
            }
            List<String> gemeentes = ondernemingen.Select(on => on.Gemeente).ToList();
            foreach (var gemeente in gemeentes)
            {
                if (GemeenteFilter.Items.Where(e => e.Equals(gemeente)).FirstOrDefault() == null)
                    GemeenteFilter.Items.Add(gemeente);
            }
            if(GlobalToken.Token != null)
            {
                Login.Visibility = Visibility.Collapsed;
                Registreer.Visibility = Visibility.Collapsed;
                beheer.Visibility = Visibility.Visible;
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
                    ondernemingenFiltered = ondernemingenFiltered.Where(onderneming => onderneming.Naam.ToLower().Contains(naamFilter)).ToList();
                }
                if (!categorieFilter.Equals(""))
                {
                    ondernemingenFiltered = ondernemingenFiltered.Where(onderneming => onderneming.Categorie.Naam.Equals(categorieFilter)).ToList();

                }
                if (!gemeenteFilter.Equals(""))
                {
                    ondernemingenFiltered = ondernemingenFiltered.Where(onderneming => onderneming.Gemeente.Equals(gemeenteFilter)).ToList();

                }
                if (isOpenFilter == true)
                {
                    ondernemingenFiltered = ondernemingenFiltered.Where(onderneming => onderneming.isOpen == true).ToList();

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

        private async void LogIn(object sender, RoutedEventArgs e)
        {
            ShowDialogAsync(false);
        }
        private async void ShowDialogAsync(bool fault)
        {
           

            var dialog = new ContentDialog()
            {
                Title = "Login",
                FullSizeDesired = false,
                MaxWidth = this.ActualWidth // Required for Mobile!
            };
            StackPanel panel = new StackPanel();
            Grid myGrid = new Grid();
            myGrid.Width = 350;
            myGrid.Height = 150;
            // Define the Columns
            ColumnDefinition colDef1 = new ColumnDefinition();
            ColumnDefinition colDef2 = new ColumnDefinition();
            myGrid.ColumnDefinitions.Add(colDef1);
            myGrid.ColumnDefinitions.Add(colDef2);

            // Define the Rows
            RowDefinition rowDef1 = new RowDefinition();
            RowDefinition rowDef2 = new RowDefinition();
            RowDefinition rowDef3 = new RowDefinition();
            myGrid.RowDefinitions.Add(rowDef1);
            myGrid.RowDefinitions.Add(rowDef2);
            myGrid.RowDefinitions.Add(rowDef3);


            // Add the first text cell to the Grid
            TextBlock txt1 = new TextBlock();
            txt1.VerticalAlignment = VerticalAlignment.Center;
            txt1.Text = "Gebruikersnaam:";
            txt1.FontSize = 15;
            txt1.Margin = new Thickness(5);
            Grid.SetColumn(txt1, 0);
            Grid.SetRow(txt1, 0);

            TextBox box1 = new TextBox();
            box1.Margin = new Thickness(5);
            box1.Height = 8;
            Grid.SetColumn(box1, 1);
            Grid.SetRow(box1, 0);

            // Add the second text cell to the Grid
            TextBlock txt2 = new TextBlock();
            txt2.Text = "Wachtwoord:";
            txt2.VerticalAlignment = VerticalAlignment.Center;
            txt2.FontSize = 15;
            txt2.Margin = new Thickness(5);
            Grid.SetColumn(txt2, 0);
            Grid.SetRow(txt2, 1);

            PasswordBox box2 = new PasswordBox();
            box2.Margin = new Thickness(5);
            box2.Height = 8;
            Grid.SetColumn(box2, 1);
            Grid.SetRow(box2, 1);

            myGrid.Children.Add(txt1);
            myGrid.Children.Add(box1);
            myGrid.Children.Add(txt2);
            myGrid.Children.Add(box2);
            panel.Children.Add(myGrid);
            if (fault == true)
            {
                TextBlock txt3 = new TextBlock();
                txt3.Text = "Wachtwoord of Gebruikersnaam fout";
                txt3.VerticalAlignment = VerticalAlignment.Center;
                txt3.FontSize = 15;
                txt3.Foreground = new SolidColorBrush(Colors.Red);
                txt3.Margin = new Thickness(5);
                // Grid.SetColumn(txt3, 0);
                //Grid.SetRow(txt3, 2);
                panel.Children.Add(txt3);
               
            }




            dialog.Content = panel;

            dialog.CloseButtonText = "Close";
            dialog.SecondaryButtonText = "Submit";

            var resultGebruiker = "";
            ContentDialogResult result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Secondary)
            {
                if (box1.Text.Trim().Equals("") || box2.Password.ToString().Trim().Equals(""))
                {

                } else
                    resultGebruiker = await gebruikerViewModel.LogInAsync(box1.Text, box2.Password.ToString());
                string[] tokens = resultGebruiker.Split('"');
                tokens[1] = "";
                if (tokens[5] == "token_type")
                {
                    
                    var token = tokens[4];
                    Gebruiker user = new Gebruiker(Int32.Parse(tokens[21].ToString()), tokens[13].ToString(), tokens[17].ToString());
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
                else
                {
                    ShowDialogAsync(true);
                }


                // Terms of use were accepted.
            }

        }

        private void BeheerAccount(object sender, RoutedEventArgs e)
        {
            if(((App)Application.Current).gebruiker.Role == "Ondernemer")
            {
                this.Frame.Navigate(typeof(BeheerOndernemerPage));
            }
        }

        private void RegistreerPage(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RegistreerPage));
        }
    }
}


    


