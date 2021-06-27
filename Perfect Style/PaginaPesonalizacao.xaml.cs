using System;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using static PersonalizationHelper;
using Windows.Foundation.Metadata;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using OEMSharedFolderAccessLib;
using Nokia.SilentInstaller.Runtime;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using Windows.System;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using Windows.Globalization.DateTimeFormatting;
using System.Diagnostics;

// Projeto Perfect Style Igor Sanches Anjatuba, MA, Brasil, Copyright © 2017
// Início do Projeto em 8 de outubro de 2017

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
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += PaginaPersonalizacao_BackPressed; //Botão voltar
  
        }
        private void DarkRB_Checked(object sender, RoutedEventArgs e)
        {
            PersonalizationHelper.AppBackgroundAccentSetter(BackgroundAccents.Dark);
            PersonalizationHelper.BackgroundAccentSetter(BackgroundAccents.Dark);
        }
        private void LightBR_Checked(object sender, RoutedEventArgs e)
        {
            PersonalizationHelper.AppBackgroundAccentSetter(BackgroundAccents.Light);
            PersonalizationHelper.BackgroundAccentSetter(BackgroundAccents.Light);
        }


        //Botão voltar
        void PaginaPersonalizacao_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
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
            if (MySplitView.IsPaneOpen == false)
            {

            }

            else if (MySplitView.IsPaneOpen == true)
            {
                MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
            }
        }
        private async void RR(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("mailto:igorsanchesinc.17@hotmail.com"));
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
        //Tema escuro e claro
        private async void B1(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = PickerViewMode.List;
            picker.SuggestedStartLocation = PickerLocationId.ComputerFolder;
            picker.FileTypeFilter.Add(".png");
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            Windows.Storage.StorageFile boot1 = await picker.PickSingleFileAsync();
            if (boot1 != null)
            {

                var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
                var BOODATA = loader.GetString("bootdata");
                var iconR = loader.GetString("BOOTW");
                {
                    UInt32 REG_SZ = 1;
                    UInt32 hKey = 1;
                    String path = "SYSTEM\\Shell\\BootScreens";
                    String key = "";
                    String value = "";
                    key = "WPBootScreenOverride";
                    value = boot1.Path;
                    rpc.rset(hKey, path, key, REG_SZ, value, 0);
                    {
                        ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                        XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                        XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                        toastTextElements[0].AppendChild(toastXml.CreateTextNode(iconR));
                        toastTextElements[1].AppendChild(toastXml.CreateTextNode(BOODATA));
                        XmlNodeList toastImageAttributes = toastXml.GetElementsByTagName("image");
                        ((XmlElement)toastImageAttributes[0]).SetAttribute("src", "ms-appx:///Assets/imageToast.png");
                        ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(toastXml));
                    }
                }
            }
        }
        private async void B2(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = PickerViewMode.List;
            picker.SuggestedStartLocation = PickerLocationId.ComputerFolder;
            picker.FileTypeFilter.Add(".png");
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            Windows.Storage.StorageFile boot1 = await picker.PickSingleFileAsync();
            if (boot1 != null)
            {

                var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
                var BOODATA = loader.GetString("bootdata");
                var iconR2 = loader.GetString("BOOT2W");
                {
                    UInt32 REG_SZ = 1;
                    UInt32 hKey = 1;
                    String path = "SYSTEM\\Shell\\BootScreens";
                    String key = "";
                    String value = "";
                    key = "WPShutDownScreenOverride";
                    value = boot1.Path;
                    rpc.rset(hKey, path, key, REG_SZ, value, 0);
                    {
                        ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                        XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                        XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                        toastTextElements[0].AppendChild(toastXml.CreateTextNode(iconR2));
                        toastTextElements[1].AppendChild(toastXml.CreateTextNode(BOODATA));
                        XmlNodeList toastImageAttributes = toastXml.GetElementsByTagName("image");
                        ((XmlElement)toastImageAttributes[0]).SetAttribute("src", "ms-appx:///Assets/imageToast.png");
                        ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(toastXml));
                    }
                }
            }
        }
        //Remover boot e reboot
        private void DB2(object sender, RoutedEventArgs e)
        {
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var data4 = loader.GetString("Data4");
            var icon4 = loader.GetString("icon4");
            {
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SYSTEM\\Shell\\BootScreens";
                String key = "";
                String value = "";
                key = "WPBootScreenOverride";
                value = "";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(icon4));
                    toastTextElements[1].AppendChild(toastXml.CreateTextNode(data4));
                    XmlNodeList toastImageAttributes = toastXml.GetElementsByTagName("image");
                    ((XmlElement)toastImageAttributes[0]).SetAttribute("src", "ms-appx:///Assets/imageToast.png");
                    ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(toastXml));
                }

            }
        }
        private void DB1(object sender, RoutedEventArgs e)
        {
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var data4 = loader.GetString("Data4");
            var icon3 = loader.GetString("icon3");
            {
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SYSTEM\\Shell\\BootScreens";
                String key = "";
                String value = "";
                key = "WPShutDownScreenOverride";
                value = "";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(icon3));
                    toastTextElements[1].AppendChild(toastXml.CreateTextNode(data4));
                    XmlNodeList toastImageAttributes = toastXml.GetElementsByTagName("image");
                    ((XmlElement)toastImageAttributes[0]).SetAttribute("src", "ms-appx:///Assets/imageToast.png");
                    ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(toastXml));
                }

            }
        }
        //Ícones da rede movel
        private async void E2G(object sender, RoutedEventArgs e)
        {
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var modefique = loader.GetString("modefique");
            var mode = loader.GetString("mode");
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var datas2 = loader.GetString("datas2");
            var text = loader.GetString("textAcao");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Shell\\OEM\\SystemTray\\DataConnectionStrings";
                String key = "";
                String value = "";
                key = "GSM_EDGE";
                value = (TextE.Text);
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode("E" + modefique));
                    toastTextElements[1].AppendChild(toastXml.CreateTextNode(mode));
                    XmlNodeList toastImageAttributes = toastXml.GetElementsByTagName("image");
                    ((XmlElement)toastImageAttributes[0]).SetAttribute("src", "ms-appx:///Assets/imageToast.png");
                    ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(toastXml));
                }
                this.Frame.Navigate(typeof(RebootPage));
            }));
            messageDialog.Commands.Add(new UICommand(label1, null, 1));
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 1;
            await messageDialog.ShowAsync();
        }
        private async void G2G(object sender, RoutedEventArgs e)
        {
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var modefique = loader.GetString("modefique");
            var mode = loader.GetString("mode");
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var datas2 = loader.GetString("datas2");
            var text = loader.GetString("textAcao");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Shell\\OEM\\SystemTray\\DataConnectionStrings";
                String key = "";
                String value = "";
                key = "GSM_GPRS";
                value = (TextG.Text);
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode("G" + modefique));
                    toastTextElements[1].AppendChild(toastXml.CreateTextNode(mode));
                    XmlNodeList toastImageAttributes = toastXml.GetElementsByTagName("image");
                    ((XmlElement)toastImageAttributes[0]).SetAttribute("src", "ms-appx:///Assets/imageToast.png");
                    ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(toastXml));
                }
                this.Frame.Navigate(typeof(RebootPage));
            }));
            messageDialog.Commands.Add(new UICommand(label1, null, 1));
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 1;
            await messageDialog.ShowAsync();
        }
        private async void N3G(object sender, RoutedEventArgs e)
        {
            Text3G.Text = (Text3G.Text);
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var modefique = loader.GetString("modefique");
            var mode = loader.GetString("mode");
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var datas2 = loader.GetString("datas2");
            var text = loader.GetString("textAcao");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Shell\\OEM\\SystemTray\\DataConnectionStrings";
                String key = "";
                String value = "";
                key = "UMTS_UMTS";
                value = (Text3G.Text);
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode("3G" + modefique));
                    toastTextElements[1].AppendChild(toastXml.CreateTextNode(mode));
                    XmlNodeList toastImageAttributes = toastXml.GetElementsByTagName("image");
                    ((XmlElement)toastImageAttributes[0]).SetAttribute("src", "ms-appx:///Assets/imageToast.png");
                    ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(toastXml));
                }
                this.Frame.Navigate(typeof(RebootPage));
            }));
            messageDialog.Commands.Add(new UICommand(label1, null, 1));
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 1;
            await messageDialog.ShowAsync();
        }
        private async void H(object sender, RoutedEventArgs e)
        {
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var modefique = loader.GetString("modefique");
            var mode = loader.GetString("mode");
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var datas2 = loader.GetString("datas2");
            var text = loader.GetString("textAcao");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Shell\\OEM\\SystemTray\\DataConnectionStrings";
                String key = "";
                String value = "";
                key = "UMTS_HSDPA";
                value = (TextH.Text);
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode("H" + modefique));
                    toastTextElements[1].AppendChild(toastXml.CreateTextNode(mode));
                    XmlNodeList toastImageAttributes = toastXml.GetElementsByTagName("image");
                    ((XmlElement)toastImageAttributes[0]).SetAttribute("src", "ms-appx:///Assets/imageToast.png");
                    ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(toastXml));
                }
                this.Frame.Navigate(typeof(RebootPage));
            }));
            messageDialog.Commands.Add(new UICommand(label1, null, 1));
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 1;
            await messageDialog.ShowAsync();
        }
        private async void HMAIS(object sender, RoutedEventArgs e)
        {
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var modefique = loader.GetString("modefique");
            var mode = loader.GetString("mode");
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var datas2 = loader.GetString("datas2");
            var text = loader.GetString("textAcao");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Shell\\OEM\\SystemTray\\DataConnectionStrings";
                String key = "";
                String value = "";
                key = "UMTS_HSPAPLUS_64QAM";
                value = (TextHMAIS.Text);
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode("H+" + modefique));
                    toastTextElements[1].AppendChild(toastXml.CreateTextNode(mode));
                    XmlNodeList toastImageAttributes = toastXml.GetElementsByTagName("image");
                    ((XmlElement)toastImageAttributes[0]).SetAttribute("src", "ms-appx:///Assets/imageToast.png");
                    ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(toastXml));
                }
                this.Frame.Navigate(typeof(RebootPage));
            }));
            messageDialog.Commands.Add(new UICommand(label1, null, 1));
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 1;
            await messageDialog.ShowAsync();
        }
        private async void N4G(object sender, RoutedEventArgs e)
        {
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var modefique = loader.GetString("modefique");
            var mode = loader.GetString("mode");
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var datas2 = loader.GetString("datas2");
            var text = loader.GetString("textAcao");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                
                UInt32 REG_DWORD = 4;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Shell\\Start";
                String key = "";
                String value = "";
                key = "TileColumnSize";
                value = (Text4G.Text);
                rpc.rset(hKey, path, key, REG_DWORD, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode("4G" + modefique));
                    toastTextElements[1].AppendChild(toastXml.CreateTextNode(mode));
                    XmlNodeList toastImageAttributes = toastXml.GetElementsByTagName("image");
                    ((XmlElement)toastImageAttributes[0]).SetAttribute("src", "ms-appx:///Assets/imageToast.png");
                    ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(toastXml));
                }
                this.Frame.Navigate(typeof(RebootPage));
            }));
            messageDialog.Commands.Add(new UICommand(label1, null, 1));
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 1;
            await messageDialog.ShowAsync();

        }
    
    }
}
