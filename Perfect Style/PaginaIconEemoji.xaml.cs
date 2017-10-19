using System;
using Windows.Foundation.Metadata;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using OEMSharedFolderAccessLib;
using Nokia.SilentInstaller.Runtime;
using Windows.Storage.Pickers;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using Windows.System;

// Projeto Font Style, Denis Fernandes, São Bernardo do Campo, SP, Brasil, Copyright © 2016
// Início do Projeto em 26 de junho de 2016

namespace Perfect_Style
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PaginaIconEemoji : Page
    {
        COEMSharedFolder rpc = new COEMSharedFolder();
        CSilentInstallerRuntime cs = new CSilentInstallerRuntime();

        public PaginaIconEemoji()
        {
            this.InitializeComponent();
            rpc.RPC_Init();
            ShowStatusBar();
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed; //Botão voltar
        }
        //Botão voltar
        void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
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
                //statusbar.ForegroundColor = Colors.WhiteSmoke;
            }
        }
        // Menu Hamburger
        private void BotaoMenu(object sender, TappedRoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }
        //Botões menu Hamburguer

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

        private void Button_Holding(object sender, HoldingRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }
        //Area emoji
        private async void BotaoEmoji1(object sender, RoutedEventArgs e)
        {
            StorageFile FontEmoji0A1 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Emoji/FontEmoji0A1.ttf"));
            StorageFile FontEmoji0B1 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Emoji/FontEmoji0B1.ttf"));
            StorageFile FontEmoji0C1 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Emoji/FontEmoji0C1.ttf"));
            StorageFile FontEmoji0D1 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Emoji/FontEmoji0D1.ttf"));
            StorageFile FontEmoji0E1 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Emoji/FontEmoji0E1.ttf"));
            StorageFile FontEmoji0F1 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Emoji/FontEmoji0F1.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(FontEmoji0A1.Path, "C:\\Windows\\FONTS\\FontEmoji0A1.ttf");
                cs.NRSCopyFile(FontEmoji0B1.Path, "C:\\Windows\\FONTS\\FontEmoji0B1.ttf");
                cs.NRSCopyFile(FontEmoji0C1.Path, "C:\\Windows\\FONTS\\FontEmoji0C1.ttf");
                cs.NRSCopyFile(FontEmoji0D1.Path, "C:\\Windows\\FONTS\\FontEmoji0D1.ttf");
                cs.NRSCopyFile(FontEmoji0E1.Path, "C:\\Windows\\FONTS\\FontEmoji0E1.ttf");
                cs.NRSCopyFile(FontEmoji0F1.Path, "C:\\Windows\\FONTS\\FontEmoji0F1.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI Emoji (TrueType)";
                value = "FontEmoji0A1.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode("Emoji Preto e Branco"));
                    toastTextElements[1].AppendChild(toastXml.CreateTextNode(data));
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
        private async void RestaurarEmoji(object sender, RoutedEventArgs e)
        {
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI Emoji (TrueType)";
                value = "seguiemj.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode("Segoe UI Emoji"));
                    toastTextElements[1].AppendChild(toastXml.CreateTextNode(data));
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
        //Area icones
        private async void ButaoIcon1(object sender, RoutedEventArgs e)
        {
            StorageFile FontIcon0A1 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Icones/FontIcon0A1.ttf"));
            StorageFile FontIcon0B1 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Icones/FontIcon0B1.ttf"));
            StorageFile FontIcon0C1 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Icones/FontIcon0C1.ttf"));
            StorageFile FontIcon0D1 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Icones/FontIcon0D1.ttf"));
            StorageFile FontIcon0E1 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Icones/FontIcon0E1.ttf"));
            StorageFile FontIcon0F1 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Icones/FontIcon0F1.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(FontIcon0A1.Path, "C:\\Windows\\FONTS\\FontIcon0A1.ttf");
                cs.NRSCopyFile(FontIcon0B1.Path, "C:\\Windows\\FONTS\\FontIcon0B1.ttf");
                cs.NRSCopyFile(FontIcon0C1.Path, "C:\\Windows\\FONTS\\FontIcon0C1.ttf");
                cs.NRSCopyFile(FontIcon0D1.Path, "C:\\Windows\\FONTS\\FontIcon0D1.ttf");
                cs.NRSCopyFile(FontIcon0E1.Path, "C:\\Windows\\FONTS\\FontIcon0E1.ttf");
                cs.NRSCopyFile(FontIcon0F1.Path, "C:\\Windows\\FONTS\\FontIcon0F1.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                value = "FontIcon0A1.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode("Ícone Android LolliPop"));
                    toastTextElements[1].AppendChild(toastXml.CreateTextNode(data));
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
        private async void BotaoIcon2(object sender, RoutedEventArgs e)

        {
            StorageFile FontIcon0A2 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Icones/FontIcon0A2.ttf"));
            StorageFile FontIcon0B2 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Icones/FontIcon0B2.ttf"));
            StorageFile FontIcon0C2 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Icones/FontIcon0C2.ttf"));
            StorageFile FontIcon0D2 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Icones/FontIcon0D2.ttf"));
            StorageFile FontIcon0E2 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Icones/FontIcon0E2.ttf"));
            StorageFile FontIcon0F2 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Icones/FontIcon0F2.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(FontIcon0A2.Path, "C:\\Windows\\FONTS\\FontIcon0A2.ttf");
                cs.NRSCopyFile(FontIcon0B2.Path, "C:\\Windows\\FONTS\\FontIcon0B2.ttf");
                cs.NRSCopyFile(FontIcon0C2.Path, "C:\\Windows\\FONTS\\FontIcon0C2.ttf");
                cs.NRSCopyFile(FontIcon0D2.Path, "C:\\Windows\\FONTS\\FontIcon0D2.ttf");
                cs.NRSCopyFile(FontIcon0E2.Path, "C:\\Windows\\FONTS\\FontIcon0E2.ttf");
                cs.NRSCopyFile(FontIcon0F2.Path, "C:\\Windows\\FONTS\\FontIcon0F2.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                value = "FontIcon0A2.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode("Ícone IOS"));
                    toastTextElements[1].AppendChild(toastXml.CreateTextNode(data));
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
        private async void BotaoIcon4(object sender, RoutedEventArgs e)
        {
            StorageFile FontIcon0A4 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Icones/FontIcon0A4.ttf"));
            StorageFile FontIcon0B4 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Icones/FontIcon0B4.ttf"));
            StorageFile FontIcon0C4 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Icones/FontIcon0C4.ttf"));
            StorageFile FontIcon0D4 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Icones/FontIcon0D4.ttf"));
            StorageFile FontIcon0E4 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Icones/FontIcon0E4.ttf"));
            StorageFile FontIcon0F4 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Icones/FontIcon0F4.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(FontIcon0A4.Path, "C:\\Windows\\FONTS\\FontIcon0A4.ttf");
                cs.NRSCopyFile(FontIcon0B4.Path, "C:\\Windows\\FONTS\\FontIcon0B4.ttf");
                cs.NRSCopyFile(FontIcon0C4.Path, "C:\\Windows\\FONTS\\FontIcon0C4.ttf");
                cs.NRSCopyFile(FontIcon0D4.Path, "C:\\Windows\\FONTS\\FontIcon0D4.ttf");
                cs.NRSCopyFile(FontIcon0E4.Path, "C:\\Windows\\FONTS\\FontIcon0E4.ttf");
                cs.NRSCopyFile(FontIcon0F4.Path, "C:\\Windows\\FONTS\\FontIcon0F4.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                value = "FontIcon0A4.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode("Ícone Color"));
                    toastTextElements[1].AppendChild(toastXml.CreateTextNode(data));
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
        private async void BotaoIcon3(object sender, RoutedEventArgs e)
        {
            StorageFile FontIcon0A3 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Icones/FontIcon0A3.ttf"));
            StorageFile FontIcon0B3 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Icones/FontIcon0B3.ttf"));
            StorageFile FontIcon0C3 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Icones/FontIcon0C3.ttf"));
            StorageFile FontIcon0D3 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Icones/FontIcon0D3.ttf"));
            StorageFile FontIcon0E3 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Icones/FontIcon0E3.ttf"));
            StorageFile FontIcon0F3 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Icones/FontIcon0F3.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(FontIcon0A3.Path, "C:\\Windows\\FONTS\\FontIcon0A3.ttf");
                cs.NRSCopyFile(FontIcon0B3.Path, "C:\\Windows\\FONTS\\FontIcon0B3.ttf");
                cs.NRSCopyFile(FontIcon0C3.Path, "C:\\Windows\\FONTS\\FontIcon0C3.ttf");
                cs.NRSCopyFile(FontIcon0D3.Path, "C:\\Windows\\FONTS\\FontIcon0D3.ttf");
                cs.NRSCopyFile(FontIcon0E3.Path, "C:\\Windows\\FONTS\\FontIcon0E3.ttf");
                cs.NRSCopyFile(FontIcon0F3.Path, "C:\\Windows\\FONTS\\FontIcon0F3.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                value = "FontIcon0A3.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode("Ícone Batéria Circular"));
                    toastTextElements[1].AppendChild(toastXml.CreateTextNode(data));
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
        private async void RestaurarIcon(object sender, RoutedEventArgs e)
        {
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe MDL2 Assets (TrueType)";
                value = "segmdl2.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode("Segoe MDL2 Assets"));
                    toastTextElements[1].AppendChild(toastXml.CreateTextNode(data));
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