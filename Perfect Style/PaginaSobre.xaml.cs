using System;
using Windows.Foundation.Metadata;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.ApplicationModel;

// Projeto Perfect Style Igor Sanches Anjatuba, MA, Brasil, Copyright © 2017
// Início do Projeto em 8 de outubro de 2017

namespace Perfect_Style
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PaginaSobre : Page
    {
        public PaginaSobre()
        {
            this.InitializeComponent();
            ShowStatusBar();
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += PaginaSobre_BackPressed; //Botão voltar
             DisplayName.Text = Package.Current.DisplayName;
        }


        public string VersionApplication
        {
            get
            {
                var VersionApplication = Windows.ApplicationModel.Package.Current.Id.Version;
                return $"{VersionApplication.Major}.{VersionApplication.Minor}.{VersionApplication.Build}.{VersionApplication.Revision}";
            }
        }
        //Botão voltar
        void PaginaSobre_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame.CanGoBack)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }
        }

        //Ativar-Desativar StatusBar - necessita da referência Windows Mobile Extensions for the UWP
        private async void ShowStatusBar()
        {
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var statusbar = StatusBar.GetForCurrentView();
                await statusbar.ShowAsync();
                //await statusbar.HideAsync();
                //statusbar.BackgroundColor = Colors.Black;
                statusbar.ForegroundColor = Windows.UI.Colors.WhiteSmoke;
            }
        }
     
        // Menu Hamburger
        private void BotaoMenu(object sender, TappedRoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }
        //Botões menu Hamburger
        private void BotaoInicio(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
        private void BotaoFontA(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PaginaLetras));
        }
        private void BotaoFontB(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PaginaIconEemoji));
        }
        private void BotaoPersonalizar(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PaginaPersonalizacao));
        }
        private void BotaoAjuda(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PaginaAjuda));
        }
        private void BotaoSobre(object sender, RoutedEventArgs e)
        {
            if (MySplitView.IsPaneOpen == false)
            {

            }

            else if (MySplitView.IsPaneOpen == true)
            {
                MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
            }
        }
        private async void Atualizacao(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://meu-windows10channel.blogspot.com.br/2016/10/verificador-de-atualizacao.html"));

        }
        private async void Feedback(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://www.meu-windows10channel.blogspot.com.br"));
        }

    
    }
}