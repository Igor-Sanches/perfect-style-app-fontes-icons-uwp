using System;
using Windows.Foundation.Metadata;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using OEMSharedFolderAccessLib;
using Nokia.SilentInstaller.Runtime;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using static PersonalizationHelper;
using Windows.System;
using Windows.UI.Core;

// Projeto Font Style, Denis Fernandes, São Bernardo do Campo, SP, Brasil, Copyright © 2016
// Início do Projeto em 26 de junho de 2016

namespace Perfect_Style
{
    public partial class PaginaPersonalizacao : Page
    {
        COEMSharedFolder rpc = new COEMSharedFolder();
        CSilentInstallerRuntime cs = new CSilentInstallerRuntime();

        public PaginaPersonalizacao()
        {
            this.InitializeComponent();
            rpc.RPC_Init();
            ShowStatusBar();
            SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested; //Botão voltar
        }
        //Botão voltar
        private void MainPage_BackRequested(object sender, BackRequestedEventArgs e)
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
        // Menu Hamburger
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
            this.Frame.Navigate(typeof(PaginaSobre));
        }
        private async void Atualizacao(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://meu-windows10channel.blogspot.com.br/2016/10/verificador-de-atualizacao.html"));

        }
        private async void Feedback(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://www.meu-windows10channel.blogspot.com.br"));
        }
        //Area personalização
        private void Button_Holding(object sender, HoldingRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }
        private void RadioLight(object sender, RoutedEventArgs e)
        {
            PersonalizationHelper.AppBackgroundAccentSetter(BackgroundAccents.Light);
            PersonalizationHelper.BackgroundAccentSetter(BackgroundAccents.Light);
        }
        private void RadioDarck(object sender, RoutedEventArgs e)
        {
            PersonalizationHelper.AppBackgroundAccentSetter(BackgroundAccents.Dark);
            PersonalizationHelper.BackgroundAccentSetter(BackgroundAccents.Dark);

        }
      
    }

}
