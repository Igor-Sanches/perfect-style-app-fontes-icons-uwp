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
using Windows.UI.Core;

// Projeto Perfect Style Igor Sanches Anjatuba, MA, Brasil, Copyright © 2017
// Início do Projeto em 8 de outubro de 2017

namespace Perfect_Style
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PaginaLetras : Page
    {
        COEMSharedFolder rpc = new COEMSharedFolder();
        CSilentInstallerRuntime cs = new CSilentInstallerRuntime();

        public PaginaLetras()
        {
            this.InitializeComponent();
            rpc.RPC_Init();
            ShowStatusBar();
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += PaginaLetras_BackPressed; //Botão voltar
        }
        //Botão voltar
        void PaginaLetras_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame.CanGoBack)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }
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
            if (MySplitView.IsPaneOpen == false)
            {

            }

            else if (MySplitView.IsPaneOpen == true)
            {
                MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
            }
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
        //Botões das fontes
        private async void AddFont1(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.List;
            picker.SuggestedStartLocation = PickerLocationId.ComputerFolder;
            picker.FileTypeFilter.Add(".ttf");
            Windows.Storage.StorageFile File1 = await picker.PickSingleFileAsync();
            if (File1 != null)
            {
                var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
                var content = loader.GetString("textContent");
                var label0 = loader.GetString("textLabel0");
                var label1 = loader.GetString("textLabel1");
                var data = loader.GetString("textData");
                var text = loader.GetString("textAcao");
                var font = loader.GetString("font2");
                var add = loader.GetString("addfont");
                var messageDialog = new MessageDialog(content, text);
                messageDialog.Commands.Add(new UICommand(label0, (command) =>
                {
                    cs.NRSCopyFile(File1.Path, "C:\\Windows\\FONTS\\FontAdd.ttf");
                    UInt32 REG_SZ = 1;
                    UInt32 hKey = 1;
                    String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                    String key = "";
                    String value = "";
                    key = "Segoe UI (TrueType)";
                    value = "FontAdd.ttf";
                    rpc.rset(hKey, path, key, REG_SZ, value, 0);
                    key = "Segoe UI Black (TrueType)";
                    value = "FontAdd.ttf";
                    rpc.rset(hKey, path, key, REG_SZ, value, 0);
                    key = "Segoe UI Bold (TrueType)";
                    value = "FontAdd.ttf";
                    rpc.rset(hKey, path, key, REG_SZ, value, 0);
                    key = "Segoe UI Light (TrueType)";
                    value = "FontAdd.ttf";
                    rpc.rset(hKey, path, key, REG_SZ, value, 0);
                    key = "Segoe UI Semibold (TrueType)";
                    value = "FontAdd.ttf";
                    rpc.rset(hKey, path, key, REG_SZ, value, 0);
                    key = "Segoe UI Semilight (TrueType)";
                    value = "FontAdd.ttf";
                    rpc.rset(hKey, path, key, REG_SZ, value, 0);
                    {
                        ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                        XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                        XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                        toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + add));
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

        private async void BotaoFont1(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA1 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A1.ttf"));
            StorageFile fileFontB1 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B1.ttf"));
            StorageFile fileFontC1 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C1.ttf"));
            StorageFile fileFontD1 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D1.ttf"));
            StorageFile fileFontE1 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E1.ttf"));
            StorageFile fileFontF1 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F1.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA1.Path, "C:\\Windows\\FONTS\\Font0A1.ttf");
                cs.NRSCopyFile(fileFontB1.Path, "C:\\Windows\\FONTS\\Font0B1.ttf");
                cs.NRSCopyFile(fileFontC1.Path, "C:\\Windows\\FONTS\\Font0C1.ttf");
                cs.NRSCopyFile(fileFontD1.Path, "C:\\Windows\\FONTS\\Font0D1.ttf");
                cs.NRSCopyFile(fileFontE1.Path, "C:\\Windows\\FONTS\\Font0E1.ttf");
                cs.NRSCopyFile(fileFontF1.Path, "C:\\Windows\\FONTS\\Font0F1.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A1.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0A1.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0A1.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0A1.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0A1.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A1.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "7Love"));
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
        private async void BotaoFont2(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA2 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A2.ttf"));
            StorageFile fileFontB2 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B2.ttf"));
            StorageFile fileFontC2 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C2.ttf"));
            StorageFile fileFontD2 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D2.ttf"));
            StorageFile fileFontE2 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E2.ttf"));
            StorageFile fileFontF2 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F2.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA2.Path, "C:\\Windows\\FONTS\\Font0A2.ttf");
                cs.NRSCopyFile(fileFontB2.Path, "C:\\Windows\\FONTS\\Font0B2.ttf");
                cs.NRSCopyFile(fileFontC2.Path, "C:\\Windows\\FONTS\\Font0C2.ttf");
                cs.NRSCopyFile(fileFontD2.Path, "C:\\Windows\\FONTS\\Font0D2.ttf");
                cs.NRSCopyFile(fileFontE2.Path, "C:\\Windows\\FONTS\\Font0E2.ttf");
                cs.NRSCopyFile(fileFontF2.Path, "C:\\Windows\\FONTS\\Font0F2.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A2.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0A2.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0A2.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0A2.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0A2.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A2.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "Art"));
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
        private async void BotaoFont3(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA3 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A3.ttf"));
            StorageFile fileFontB3 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B3.ttf"));
            StorageFile fileFontC3 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C3.ttf"));
            StorageFile fileFontD3 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D3.ttf"));
            StorageFile fileFontE3 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E3.ttf"));
            StorageFile fileFontF3 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F3.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA3.Path, "C:\\Windows\\FONTS\\Font0A3.ttf");
                cs.NRSCopyFile(fileFontB3.Path, "C:\\Windows\\FONTS\\Font0B3.ttf");
                cs.NRSCopyFile(fileFontC3.Path, "C:\\Windows\\FONTS\\Font0C3.ttf");
                cs.NRSCopyFile(fileFontD3.Path, "C:\\Windows\\FONTS\\Font0D3.ttf");
                cs.NRSCopyFile(fileFontE3.Path, "C:\\Windows\\FONTS\\Font0E3.ttf");
                cs.NRSCopyFile(fileFontF3.Path, "C:\\Windows\\FONTS\\Font0F3.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A3.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0A3.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0A3.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0A3.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0A3.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A3.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "Digital"));
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
        private async void BotaoFont4(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA4 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A4.ttf"));
            StorageFile fileFontB4 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B4.ttf"));
            StorageFile fileFontC4 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C4.ttf"));
            StorageFile fileFontD4 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D4.ttf"));
            StorageFile fileFontE4 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E4.ttf"));
            StorageFile fileFontF4 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F4.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA4.Path, "C:\\Windows\\FONTS\\Font0A4.ttf");
                cs.NRSCopyFile(fileFontB4.Path, "C:\\Windows\\FONTS\\Font0B4.ttf");
                cs.NRSCopyFile(fileFontC4.Path, "C:\\Windows\\FONTS\\Font0C4.ttf");
                cs.NRSCopyFile(fileFontD4.Path, "C:\\Windows\\FONTS\\Font0D4.ttf");
                cs.NRSCopyFile(fileFontE4.Path, "C:\\Windows\\FONTS\\Font0E4.ttf");
                cs.NRSCopyFile(fileFontF4.Path, "C:\\Windows\\FONTS\\Font0F4.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A4.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0A4.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0A4.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0A4.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0A4.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A4.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "Bailey"));
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
        private async void BotaoFont6(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA6 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A6.ttf"));
            StorageFile fileFontB6 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B6.ttf"));
            StorageFile fileFontC6 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C6.ttf"));
            StorageFile fileFontD6 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D6.ttf"));
            StorageFile fileFontE6 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E6.ttf"));
            StorageFile fileFontF6 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F6.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA6.Path, "C:\\Windows\\FONTS\\Font0A6.ttf");
                cs.NRSCopyFile(fileFontB6.Path, "C:\\Windows\\FONTS\\Font0B6.ttf");
                cs.NRSCopyFile(fileFontC6.Path, "C:\\Windows\\FONTS\\Font0C6.ttf");
                cs.NRSCopyFile(fileFontD6.Path, "C:\\Windows\\FONTS\\Font0D6.ttf");
                cs.NRSCopyFile(fileFontE6.Path, "C:\\Windows\\FONTS\\Font0E6.ttf");
                cs.NRSCopyFile(fileFontF6.Path, "C:\\Windows\\FONTS\\Font0F6.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A6.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0A6.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0A6.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0A6.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0A6.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A6.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "Beahausm Segoe"));
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
        private async void BotaoFont7(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA7 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A7.ttf"));
            StorageFile fileFontB7 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B7.ttf"));
            StorageFile fileFontC7 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C7.ttf"));
            StorageFile fileFontD7 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D7.ttf"));
            StorageFile fileFontE7 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E7.ttf"));
            StorageFile fileFontF7 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F7.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA7.Path, "C:\\Windows\\FONTS\\Font0A7.ttf");
                cs.NRSCopyFile(fileFontB7.Path, "C:\\Windows\\FONTS\\Font0B7.ttf");
                cs.NRSCopyFile(fileFontC7.Path, "C:\\Windows\\FONTS\\Font0C7.ttf");
                cs.NRSCopyFile(fileFontD7.Path, "C:\\Windows\\FONTS\\Font0D7.ttf");
                cs.NRSCopyFile(fileFontE7.Path, "C:\\Windows\\FONTS\\Font0E7.ttf");
                cs.NRSCopyFile(fileFontF7.Path, "C:\\Windows\\FONTS\\Font0F7.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A7.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0A7.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0A7.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0A7.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0A7.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A7.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "Coke Segoe"));
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
        private async void BotaoFont8(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA8 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A8.ttf"));
            StorageFile fileFontB8 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B8.ttf"));
            StorageFile fileFontC8 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C8.ttf"));
            StorageFile fileFontD8 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D8.ttf"));
            StorageFile fileFontE8 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E8.ttf"));
            StorageFile fileFontF8 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F8.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA8.Path, "C:\\Windows\\FONTS\\Font0A8.ttf");
                cs.NRSCopyFile(fileFontB8.Path, "C:\\Windows\\FONTS\\Font0B8.ttf");
                cs.NRSCopyFile(fileFontC8.Path, "C:\\Windows\\FONTS\\Font0C8.ttf");
                cs.NRSCopyFile(fileFontD8.Path, "C:\\Windows\\FONTS\\Font0D8.ttf");
                cs.NRSCopyFile(fileFontE8.Path, "C:\\Windows\\FONTS\\Font0E8.ttf");
                cs.NRSCopyFile(fileFontF8.Path, "C:\\Windows\\FONTS\\Font0F8.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A8.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0A8.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0A8.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0A8.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0A8.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A8.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "Cupid"));
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
        private async void BotaoFont10(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA10 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A10.ttf"));
            StorageFile fileFontB10 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B10.ttf"));
            StorageFile fileFontC10 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C10.ttf"));
            StorageFile fileFontD10 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D10.ttf"));
            StorageFile fileFontE10 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E10.ttf"));
            StorageFile fileFontF10 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F10.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA10.Path, "C:\\Windows\\FONTS\\Font0A10.ttf");
                cs.NRSCopyFile(fileFontB10.Path, "C:\\Windows\\FONTS\\Font0B10.ttf");
                cs.NRSCopyFile(fileFontC10.Path, "C:\\Windows\\FONTS\\Font0C10.ttf");
                cs.NRSCopyFile(fileFontD10.Path, "C:\\Windows\\FONTS\\Font0D10.ttf");
                cs.NRSCopyFile(fileFontE10.Path, "C:\\Windows\\FONTS\\Font0E10.ttf");
                cs.NRSCopyFile(fileFontF10.Path, "C:\\Windows\\FONTS\\Font0F10.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A10.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0A10.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0A10.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0A10.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0A10.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A10.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "DIN Altemete"));
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
        private async void BotaoFont12(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA12 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A12.ttf"));
            StorageFile fileFontB12 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B12.ttf"));
            StorageFile fileFontC12 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C12.ttf"));
            StorageFile fileFontD12 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D12.ttf"));
            StorageFile fileFontE12 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E12.ttf"));
            StorageFile fileFontF12 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F12.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA12.Path, "C:\\Windows\\FONTS\\Font0A12.ttf");
                cs.NRSCopyFile(fileFontB12.Path, "C:\\Windows\\FONTS\\Font0B12.ttf");
                cs.NRSCopyFile(fileFontC12.Path, "C:\\Windows\\FONTS\\Font0C12.ttf");
                cs.NRSCopyFile(fileFontD12.Path, "C:\\Windows\\FONTS\\Font0D12.ttf");
                cs.NRSCopyFile(fileFontE12.Path, "C:\\Windows\\FONTS\\Font0E12.ttf");
                cs.NRSCopyFile(fileFontF12.Path, "C:\\Windows\\FONTS\\Font0F12.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A12.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0B12.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0C12.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0D12.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0E12.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A12.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "Buxton Sketch"));
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
        private async void BotaoFont13(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA13 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A13.ttf"));
            StorageFile fileFontB13 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B13.ttf"));
            StorageFile fileFontC13 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C13.ttf"));
            StorageFile fileFontD13 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D13.ttf"));
            StorageFile fileFontE13 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E13.ttf"));
            StorageFile fileFontF13 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F13.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA13.Path, "C:\\Windows\\FONTS\\Font0A13.ttf");
                cs.NRSCopyFile(fileFontB13.Path, "C:\\Windows\\FONTS\\Font0B13.ttf");
                cs.NRSCopyFile(fileFontC13.Path, "C:\\Windows\\FONTS\\Font0C13.ttf");
                cs.NRSCopyFile(fileFontD13.Path, "C:\\Windows\\FONTS\\Font0D13.ttf");
                cs.NRSCopyFile(fileFontE13.Path, "C:\\Windows\\FONTS\\Font0E13.ttf");
                cs.NRSCopyFile(fileFontF13.Path, "C:\\Windows\\FONTS\\Font0F13.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A13.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0B13.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0C13.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0D13.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0E13.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A13.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "LT Oksana"));
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
        private async void BotaoFont14(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA14 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A14.ttf"));
            StorageFile fileFontB14 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B14.ttf"));
            StorageFile fileFontC14 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C14.ttf"));
            StorageFile fileFontD14 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D14.ttf"));
            StorageFile fileFontE14 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E14.ttf"));
            StorageFile fileFontF14 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F14.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA14.Path, "C:\\Windows\\FONTS\\Font0A14.ttf");
                cs.NRSCopyFile(fileFontB14.Path, "C:\\Windows\\FONTS\\Font0B14.ttf");
                cs.NRSCopyFile(fileFontC14.Path, "C:\\Windows\\FONTS\\Font0C14.ttf");
                cs.NRSCopyFile(fileFontD14.Path, "C:\\Windows\\FONTS\\Font0D14.ttf");
                cs.NRSCopyFile(fileFontE14.Path, "C:\\Windows\\FONTS\\Font0E14.ttf");
                cs.NRSCopyFile(fileFontF14.Path, "C:\\Windows\\FONTS\\Font0F14.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A14.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0B14.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0C14.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0D14.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0E14.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A14.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "MV Boli"));
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
        private async void BotaoFont15(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA15 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A15.ttf"));
            StorageFile fileFontB15 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B15.ttf"));
            StorageFile fileFontC15 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C15.ttf"));
            StorageFile fileFontD15 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D15.ttf"));
            StorageFile fileFontE15 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E15.ttf"));
            StorageFile fileFontF15 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F15.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA15.Path, "C:\\Windows\\FONTS\\Font0A15.ttf");
                cs.NRSCopyFile(fileFontB15.Path, "C:\\Windows\\FONTS\\Font0B15.ttf");
                cs.NRSCopyFile(fileFontC15.Path, "C:\\Windows\\FONTS\\Font0C15.ttf");
                cs.NRSCopyFile(fileFontD15.Path, "C:\\Windows\\FONTS\\Font0D15.ttf");
                cs.NRSCopyFile(fileFontE15.Path, "C:\\Windows\\FONTS\\Font0E15.ttf");
                cs.NRSCopyFile(fileFontF15.Path, "C:\\Windows\\FONTS\\Font0F15.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A15.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0A15.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0A15.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0A15.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0A15.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A15.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "Monotype"));
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
        private async void BotaoFont16(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA16 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A16.ttf"));
            StorageFile fileFontB16 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B16.ttf"));
            StorageFile fileFontC16 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C16.ttf"));
            StorageFile fileFontD16 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D16.ttf"));
            StorageFile fileFontE16 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E16.ttf"));
            StorageFile fileFontF16 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F16.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA16.Path, "C:\\Windows\\FONTS\\Font0A16.ttf");
                cs.NRSCopyFile(fileFontB16.Path, "C:\\Windows\\FONTS\\Font0B16.ttf");
                cs.NRSCopyFile(fileFontC16.Path, "C:\\Windows\\FONTS\\Font0C16.ttf");
                cs.NRSCopyFile(fileFontD16.Path, "C:\\Windows\\FONTS\\Font0D16.ttf");
                cs.NRSCopyFile(fileFontE16.Path, "C:\\Windows\\FONTS\\Font0E16.ttf");
                cs.NRSCopyFile(fileFontF16.Path, "C:\\Windows\\FONTS\\Font0F16.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A16.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0A16.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0A16.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0A16.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0A16.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A16.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "SketchFlow"));
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
        private async void BotaoFont17(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA17 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A17.ttf"));
            StorageFile fileFontB17 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B17.ttf"));
            StorageFile fileFontC17 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C17.ttf"));
            StorageFile fileFontD17 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D17.ttf"));
            StorageFile fileFontE17 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E17.ttf"));
            StorageFile fileFontF17 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F17.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA17.Path, "C:\\Windows\\FONTS\\Font0A17.ttf");
                cs.NRSCopyFile(fileFontB17.Path, "C:\\Windows\\FONTS\\Font0B17.ttf");
                cs.NRSCopyFile(fileFontC17.Path, "C:\\Windows\\FONTS\\Font0C17.ttf");
                cs.NRSCopyFile(fileFontD17.Path, "C:\\Windows\\FONTS\\Font0D17.ttf");
                cs.NRSCopyFile(fileFontE17.Path, "C:\\Windows\\FONTS\\Font0E17.ttf");
                cs.NRSCopyFile(fileFontF17.Path, "C:\\Windows\\FONTS\\Font0F17.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A17.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0A17.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0A17.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0A17.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0A17.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A17.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "Ubuntu Light"));
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
        private async void BotaoFont18(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA18 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A18.ttf"));
            StorageFile fileFontB18 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B18.ttf"));
            StorageFile fileFontC18 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C18.ttf"));
            StorageFile fileFontD18 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D18.ttf"));
            StorageFile fileFontE18 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E18.ttf"));
            StorageFile fileFontF18 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F18.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA18.Path, "C:\\Windows\\FONTS\\Font0A18.ttf");
                cs.NRSCopyFile(fileFontB18.Path, "C:\\Windows\\FONTS\\Font0B18.ttf");
                cs.NRSCopyFile(fileFontC18.Path, "C:\\Windows\\FONTS\\Font0C18.ttf");
                cs.NRSCopyFile(fileFontD18.Path, "C:\\Windows\\FONTS\\Font0D18.ttf");
                cs.NRSCopyFile(fileFontE18.Path, "C:\\Windows\\FONTS\\Font0E18.ttf");
                cs.NRSCopyFile(fileFontF18.Path, "C:\\Windows\\FONTS\\Font0F18.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A18.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0A18.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0A18.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0A18.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0A18.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A18.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "San Francisco"));
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
        private async void BotaoFont19(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA19 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A19.ttf"));
            StorageFile fileFontB19 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B19.ttf"));
            StorageFile fileFontC19 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C19.ttf"));
            StorageFile fileFontD19 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D19.ttf"));
            StorageFile fileFontE19 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E19.ttf"));
            StorageFile fileFontF19 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F19.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA19.Path, "C:\\Windows\\FONTS\\Font0A19.ttf");
                cs.NRSCopyFile(fileFontB19.Path, "C:\\Windows\\FONTS\\Font0B19.ttf");
                cs.NRSCopyFile(fileFontC19.Path, "C:\\Windows\\FONTS\\Font0C19.ttf");
                cs.NRSCopyFile(fileFontD19.Path, "C:\\Windows\\FONTS\\Font0D19.ttf");
                cs.NRSCopyFile(fileFontE19.Path, "C:\\Windows\\FONTS\\Font0E19.ttf");
                cs.NRSCopyFile(fileFontF19.Path, "C:\\Windows\\FONTS\\Font0F19.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A19.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0A19.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0A19.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0A19.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0A19.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A19.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "Papyrus"));
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
        private async void BotaoFont20(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA20 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A20.ttf"));
            StorageFile fileFontB20 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B20.ttf"));
            StorageFile fileFontC20 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C20.ttf"));
            StorageFile fileFontD20 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D20.ttf"));
            StorageFile fileFontE20 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E20.ttf"));
            StorageFile fileFontF20 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F20.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA20.Path, "C:\\Windows\\FONTS\\Font0A20.ttf");
                cs.NRSCopyFile(fileFontB20.Path, "C:\\Windows\\FONTS\\Font0B20.ttf");
                cs.NRSCopyFile(fileFontC20.Path, "C:\\Windows\\FONTS\\Font0C20.ttf");
                cs.NRSCopyFile(fileFontD20.Path, "C:\\Windows\\FONTS\\Font0D20.ttf");
                cs.NRSCopyFile(fileFontE20.Path, "C:\\Windows\\FONTS\\Font0E20.ttf");
                cs.NRSCopyFile(fileFontF20.Path, "C:\\Windows\\FONTS\\Font0F20.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A20.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0A20.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0A20.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0A20.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0A20.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A20.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "Tempus Sans ITC"));
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
        private async void BotaoFont21(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA21 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A21.ttf"));
            StorageFile fileFontB21 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B21.ttf"));
            StorageFile fileFontC21 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C21.ttf"));
            StorageFile fileFontD21 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D21.ttf"));
            StorageFile fileFontE21 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E21.ttf"));
            StorageFile fileFontF21 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F21.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA21.Path, "C:\\Windows\\FONTS\\Font0A21.ttf");
                cs.NRSCopyFile(fileFontB21.Path, "C:\\Windows\\FONTS\\Font0B21.ttf");
                cs.NRSCopyFile(fileFontC21.Path, "C:\\Windows\\FONTS\\Font0C21.ttf");
                cs.NRSCopyFile(fileFontD21.Path, "C:\\Windows\\FONTS\\Font0D21.ttf");
                cs.NRSCopyFile(fileFontE21.Path, "C:\\Windows\\FONTS\\Font0E21.ttf");
                cs.NRSCopyFile(fileFontF21.Path, "C:\\Windows\\FONTS\\Font0F21.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A21.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0A21.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0A21.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0A21.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0A21.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A21.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "Neo Sans ITD"));
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
        private async void BotaoFont22(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA22 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A22.ttf"));
            StorageFile fileFontB22 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B22.ttf"));
            StorageFile fileFontC22 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C22.ttf"));
            StorageFile fileFontD22 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D22.ttf"));
            StorageFile fileFontE22 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E22.ttf"));
            StorageFile fileFontF22 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F22.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA22.Path, "C:\\Windows\\FONTS\\Font0A22.ttf");
                cs.NRSCopyFile(fileFontB22.Path, "C:\\Windows\\FONTS\\Font0B22.ttf");
                cs.NRSCopyFile(fileFontC22.Path, "C:\\Windows\\FONTS\\Font0C22.ttf");
                cs.NRSCopyFile(fileFontD22.Path, "C:\\Windows\\FONTS\\Font0D22.ttf");
                cs.NRSCopyFile(fileFontE22.Path, "C:\\Windows\\FONTS\\Font0E22.ttf");
                cs.NRSCopyFile(fileFontF22.Path, "C:\\Windows\\FONTS\\Font0F22.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A22.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0A22.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0A22.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0A22.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0A22.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A22.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "Mad's Scrawl"));
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
        private async void BotaoFont23(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA23 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A23.ttf"));
            StorageFile fileFontB23 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B23.ttf"));
            StorageFile fileFontC23 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C23.ttf"));
            StorageFile fileFontD23 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D23.ttf"));
            StorageFile fileFontE23 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E23.ttf"));
            StorageFile fileFontF23 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F23.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA23.Path, "C:\\Windows\\FONTS\\Font0A23.ttf");
                cs.NRSCopyFile(fileFontB23.Path, "C:\\Windows\\FONTS\\Font0B23.ttf");
                cs.NRSCopyFile(fileFontC23.Path, "C:\\Windows\\FONTS\\Font0C23.ttf");
                cs.NRSCopyFile(fileFontD23.Path, "C:\\Windows\\FONTS\\Font0D23.ttf");
                cs.NRSCopyFile(fileFontE23.Path, "C:\\Windows\\FONTS\\Font0E23.ttf");
                cs.NRSCopyFile(fileFontF23.Path, "C:\\Windows\\FONTS\\Font0F23.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A23.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0A23.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0A23.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0A23.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0A23.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A23.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "Xoxoxa"));
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
        private async void BotaoFont24(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA24 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A24.ttf"));
            StorageFile fileFontB24 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B24.ttf"));
            StorageFile fileFontC24 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C24.ttf"));
            StorageFile fileFontD24 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D24.ttf"));
            StorageFile fileFontE24 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E24.ttf"));
            StorageFile fileFontF24 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F24.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA24.Path, "C:\\Windows\\FONTS\\Font0A24.ttf");
                cs.NRSCopyFile(fileFontB24.Path, "C:\\Windows\\FONTS\\Font0B24.ttf");
                cs.NRSCopyFile(fileFontC24.Path, "C:\\Windows\\FONTS\\Font0C24.ttf");
                cs.NRSCopyFile(fileFontD24.Path, "C:\\Windows\\FONTS\\Font0D24.ttf");
                cs.NRSCopyFile(fileFontE24.Path, "C:\\Windows\\FONTS\\Font0E24.ttf");
                cs.NRSCopyFile(fileFontF24.Path, "C:\\Windows\\FONTS\\Font0F24.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A24.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0A24.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0A24.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0A24.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0A24.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A24.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "Valentin"));
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
        private async void BotaoFont25(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA25 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A25.ttf"));
            StorageFile fileFontB25 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B25.ttf"));
            StorageFile fileFontC25 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C25.ttf"));
            StorageFile fileFontD25 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D25.ttf"));
            StorageFile fileFontE25 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E25.ttf"));
            StorageFile fileFontF25 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F25.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA25.Path, "C:\\Windows\\FONTS\\Font0A25.ttf");
                cs.NRSCopyFile(fileFontB25.Path, "C:\\Windows\\FONTS\\Font0B25.ttf");
                cs.NRSCopyFile(fileFontC25.Path, "C:\\Windows\\FONTS\\Font0C25.ttf");
                cs.NRSCopyFile(fileFontD25.Path, "C:\\Windows\\FONTS\\Font0D25.ttf");
                cs.NRSCopyFile(fileFontE25.Path, "C:\\Windows\\FONTS\\Font0E25.ttf");
                cs.NRSCopyFile(fileFontF25.Path, "C:\\Windows\\FONTS\\Font0F25.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A25.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0A25.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0A25.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0A25.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0A25.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A25.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "Choco Cooky"));
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
        private async void BotaoFont26(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA26 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A26.ttf"));
            StorageFile fileFontB26 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B26.ttf"));
            StorageFile fileFontC26 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C26.ttf"));
            StorageFile fileFontD26 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D26.ttf"));
            StorageFile fileFontE26 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E26.ttf"));
            StorageFile fileFontF26 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F26.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA26.Path, "C:\\Windows\\FONTS\\Font0A26.ttf");
                cs.NRSCopyFile(fileFontB26.Path, "C:\\Windows\\FONTS\\Font0B26.ttf");
                cs.NRSCopyFile(fileFontC26.Path, "C:\\Windows\\FONTS\\Font0C26.ttf");
                cs.NRSCopyFile(fileFontD26.Path, "C:\\Windows\\FONTS\\Font0D26.ttf");
                cs.NRSCopyFile(fileFontE26.Path, "C:\\Windows\\FONTS\\Font0E26.ttf");
                cs.NRSCopyFile(fileFontF26.Path, "C:\\Windows\\FONTS\\Font0F26.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A26.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0A26.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0A26.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0A26.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0A26.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A26.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "Vera"));
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
        private async void BotaoFont27(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA27 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A27.ttf"));
            StorageFile fileFontB27 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B27.ttf"));
            StorageFile fileFontC27 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C27.ttf"));
            StorageFile fileFontD27 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D27.ttf"));
            StorageFile fileFontE27 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E27.ttf"));
            StorageFile fileFontF27 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F27.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA27.Path, "C:\\Windows\\FONTS\\Font0A27.ttf");
                cs.NRSCopyFile(fileFontB27.Path, "C:\\Windows\\FONTS\\Font0B27.ttf");
                cs.NRSCopyFile(fileFontC27.Path, "C:\\Windows\\FONTS\\Font0C27.ttf");
                cs.NRSCopyFile(fileFontD27.Path, "C:\\Windows\\FONTS\\Font0D27.ttf");
                cs.NRSCopyFile(fileFontE27.Path, "C:\\Windows\\FONTS\\Font0E27.ttf");
                cs.NRSCopyFile(fileFontF27.Path, "C:\\Windows\\FONTS\\Font0F27.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A27.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0A27.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0A27.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0A27.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0A27.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A27.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "Calibri"));
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
        private async void BotaoFont28(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA28 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A28.ttf"));
            StorageFile fileFontB28 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B28.ttf"));
            StorageFile fileFontC28 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C28.ttf"));
            StorageFile fileFontD28 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D28.ttf"));
            StorageFile fileFontE28 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E28.ttf"));
            StorageFile fileFontF28 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F28.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA28.Path, "C:\\Windows\\FONTS\\Font0A28.ttf");
                cs.NRSCopyFile(fileFontB28.Path, "C:\\Windows\\FONTS\\Font0B28.ttf");
                cs.NRSCopyFile(fileFontC28.Path, "C:\\Windows\\FONTS\\Font0C28.ttf");
                cs.NRSCopyFile(fileFontD28.Path, "C:\\Windows\\FONTS\\Font0D28.ttf");
                cs.NRSCopyFile(fileFontE28.Path, "C:\\Windows\\FONTS\\Font0E28.ttf");
                cs.NRSCopyFile(fileFontF28.Path, "C:\\Windows\\FONTS\\Font0F28.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A28.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0A28.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0A28.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0A28.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0A28.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A28.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "Catholic School"));
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
        private async void BotaoFont29(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA29 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A29.ttf"));
            StorageFile fileFontB29 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B29.ttf"));
            StorageFile fileFontC29 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C29.ttf"));
            StorageFile fileFontD29 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D29.ttf"));
            StorageFile fileFontE29 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E29.ttf"));
            StorageFile fileFontF29 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F29.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA29.Path, "C:\\Windows\\FONTS\\Font0A29.ttf");
                cs.NRSCopyFile(fileFontB29.Path, "C:\\Windows\\FONTS\\Font0B29.ttf");
                cs.NRSCopyFile(fileFontC29.Path, "C:\\Windows\\FONTS\\Font0C29.ttf");
                cs.NRSCopyFile(fileFontD29.Path, "C:\\Windows\\FONTS\\Font0D29.ttf");
                cs.NRSCopyFile(fileFontE29.Path, "C:\\Windows\\FONTS\\Font0E29.ttf");
                cs.NRSCopyFile(fileFontF29.Path, "C:\\Windows\\FONTS\\Font0F29.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A29.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0A29.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0A29.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0A29.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0A29.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A29.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "Oswald Light"));
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
        private async void BotaoFont30(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA30 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A31.ttf"));
            StorageFile fileFontB30 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B31.ttf"));
            StorageFile fileFontC30 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C31.ttf"));
            StorageFile fileFontD30 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D31.ttf"));
            StorageFile fileFontE30 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E31.ttf"));
            StorageFile fileFontF30 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F31.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA30.Path, "C:\\Windows\\FONTS\\Font0A30.ttf");
                cs.NRSCopyFile(fileFontB30.Path, "C:\\Windows\\FONTS\\Font0B30.ttf");
                cs.NRSCopyFile(fileFontC30.Path, "C:\\Windows\\FONTS\\Font0C30.ttf");
                cs.NRSCopyFile(fileFontD30.Path, "C:\\Windows\\FONTS\\Font0D30.ttf");
                cs.NRSCopyFile(fileFontE30.Path, "C:\\Windows\\FONTS\\Font0E30.ttf");
                cs.NRSCopyFile(fileFontF30.Path, "C:\\Windows\\FONTS\\Font0F30.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A30.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0A30.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0A30.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0A30.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0A30.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A30.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "Willow"));
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
        private async void BotaoFont31(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA31 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A31.ttf"));
            StorageFile fileFontB31 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B31.ttf"));
            StorageFile fileFontC31 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C31.ttf"));
            StorageFile fileFontD31 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D31.ttf"));
            StorageFile fileFontE31 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E31.ttf"));
            StorageFile fileFontF31 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F31.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA31.Path, "C:\\Windows\\FONTS\\Font0A31.ttf");
                cs.NRSCopyFile(fileFontB31.Path, "C:\\Windows\\FONTS\\Font0B31.ttf");
                cs.NRSCopyFile(fileFontC31.Path, "C:\\Windows\\FONTS\\Font0C31.ttf");
                cs.NRSCopyFile(fileFontD31.Path, "C:\\Windows\\FONTS\\Font0D31.ttf");
                cs.NRSCopyFile(fileFontE31.Path, "C:\\Windows\\FONTS\\Font0E31.ttf");
                cs.NRSCopyFile(fileFontF31.Path, "C:\\Windows\\FONTS\\Font0F31.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A31.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0A31.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0A31.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0A31.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0A31.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A31.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "Edgy"));
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
        private async void BotaoFont32(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA32 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A32.ttf"));
            StorageFile fileFontB32 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B32.ttf"));
            StorageFile fileFontC32 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C32.ttf"));
            StorageFile fileFontD32 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D32.ttf"));
            StorageFile fileFontE32 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E32.ttf"));
            StorageFile fileFontF32 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F32.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA32.Path, "C:\\Windows\\FONTS\\Font0A32.ttf");
                cs.NRSCopyFile(fileFontB32.Path, "C:\\Windows\\FONTS\\Font0B32.ttf");
                cs.NRSCopyFile(fileFontC32.Path, "C:\\Windows\\FONTS\\Font0C32.ttf");
                cs.NRSCopyFile(fileFontD32.Path, "C:\\Windows\\FONTS\\Font0D32.ttf");
                cs.NRSCopyFile(fileFontE32.Path, "C:\\Windows\\FONTS\\Font0E32.ttf");
                cs.NRSCopyFile(fileFontF32.Path, "C:\\Windows\\FONTS\\Font0F32.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A32.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0A32.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0A32.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0A32.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0A32.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A32.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "Harry Potter"));
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
        private async void BotaoFont34(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA34 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A34.ttf"));
            StorageFile fileFontB34 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B34.ttf"));
            StorageFile fileFontC34 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C34.ttf"));
            StorageFile fileFontD34 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D34.ttf"));
            StorageFile fileFontE34 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E34.ttf"));
            StorageFile fileFontF34 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F34.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA34.Path, "C:\\Windows\\FONTS\\Font0A34.ttf");
                cs.NRSCopyFile(fileFontB34.Path, "C:\\Windows\\FONTS\\Font0B34.ttf");
                cs.NRSCopyFile(fileFontC34.Path, "C:\\Windows\\FONTS\\Font0C34.ttf");
                cs.NRSCopyFile(fileFontD34.Path, "C:\\Windows\\FONTS\\Font0D34.ttf");
                cs.NRSCopyFile(fileFontE34.Path, "C:\\Windows\\FONTS\\Font0E34.ttf");
                cs.NRSCopyFile(fileFontF34.Path, "C:\\Windows\\FONTS\\Font0F34.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A34.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0A34.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0A34.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0A34.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0A34.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A34.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "Helvetica Segoe"));
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
        private async void BotaoFont35(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA35 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A35.ttf"));
            StorageFile fileFontB35 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B35.ttf"));
            StorageFile fileFontC35 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C35.ttf"));
            StorageFile fileFontD35 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D35.ttf"));
            StorageFile fileFontE35 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E35.ttf"));
            StorageFile fileFontF35 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F35.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA35.Path, "C:\\Windows\\FONTS\\Font0A35.ttf");
                cs.NRSCopyFile(fileFontB35.Path, "C:\\Windows\\FONTS\\Font0B35.ttf");
                cs.NRSCopyFile(fileFontC35.Path, "C:\\Windows\\FONTS\\Font0C35.ttf");
                cs.NRSCopyFile(fileFontD35.Path, "C:\\Windows\\FONTS\\Font0D35.ttf");
                cs.NRSCopyFile(fileFontE35.Path, "C:\\Windows\\FONTS\\Font0E35.ttf");
                cs.NRSCopyFile(fileFontF35.Path, "C:\\Windows\\FONTS\\Font0F35.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A35.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0A35.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0A35.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0A35.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0A35.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A35.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "Iciel Aline"));
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
        private async void BotaoFont36(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA36 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A36.ttf"));
            StorageFile fileFontB36 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B36.ttf"));
            StorageFile fileFontC36 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C36.ttf"));
            StorageFile fileFontD36 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D36.ttf"));
            StorageFile fileFontE36 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E36.ttf"));
            StorageFile fileFontF36 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F36.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA36.Path, "C:\\Windows\\FONTS\\Font0A36.ttf");
                cs.NRSCopyFile(fileFontB36.Path, "C:\\Windows\\FONTS\\Font0B36.ttf");
                cs.NRSCopyFile(fileFontC36.Path, "C:\\Windows\\FONTS\\Font0C36.ttf");
                cs.NRSCopyFile(fileFontD36.Path, "C:\\Windows\\FONTS\\Font0D36.ttf");
                cs.NRSCopyFile(fileFontE36.Path, "C:\\Windows\\FONTS\\Font0E36.ttf");
                cs.NRSCopyFile(fileFontF36.Path, "C:\\Windows\\FONTS\\Font0F36.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A36.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0A36.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0A36.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0A36.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0A36.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A36.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "Kids"));
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
        private async void BotaoFont37(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA37 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A37.ttf"));
            StorageFile fileFontB37 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B37.ttf"));
            StorageFile fileFontC37 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C37.ttf"));
            StorageFile fileFontD37 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D37.ttf"));
            StorageFile fileFontE37 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E37.ttf"));
            StorageFile fileFontF37 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F37.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA37.Path, "C:\\Windows\\FONTS\\Font0A37.ttf");
                cs.NRSCopyFile(fileFontB37.Path, "C:\\Windows\\FONTS\\Font0B37.ttf");
                cs.NRSCopyFile(fileFontC37.Path, "C:\\Windows\\FONTS\\Font0C37.ttf");
                cs.NRSCopyFile(fileFontD37.Path, "C:\\Windows\\FONTS\\Font0D37.ttf");
                cs.NRSCopyFile(fileFontE37.Path, "C:\\Windows\\FONTS\\Font0E37.ttf");
                cs.NRSCopyFile(fileFontF37.Path, "C:\\Windows\\FONTS\\Font0F37.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A37.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0A37.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0A37.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0A37.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0A37.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A37.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "Lilly"));
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
        private async void BotaoFont38(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA38 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A38.ttf"));
            StorageFile fileFontB38 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B38.ttf"));
            StorageFile fileFontC38 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C38.ttf"));
            StorageFile fileFontD38 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D38.ttf"));
            StorageFile fileFontE38 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E38.ttf"));
            StorageFile fileFontF38 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F38.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA38.Path, "C:\\Windows\\FONTS\\Font0A38.ttf");
                cs.NRSCopyFile(fileFontB38.Path, "C:\\Windows\\FONTS\\Font0B38.ttf");
                cs.NRSCopyFile(fileFontC38.Path, "C:\\Windows\\FONTS\\Font0C38.ttf");
                cs.NRSCopyFile(fileFontD38.Path, "C:\\Windows\\FONTS\\Font0D38.ttf");
                cs.NRSCopyFile(fileFontE38.Path, "C:\\Windows\\FONTS\\Font0E38.ttf");
                cs.NRSCopyFile(fileFontF38.Path, "C:\\Windows\\FONTS\\Font0F38.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A38.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0A38.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0A38.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0A38.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0A38.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A38.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "LobsterTwo"));
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
        private async void BotaoFont39(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA39 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A39.ttf"));
            StorageFile fileFontB39 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B39.ttf"));
            StorageFile fileFontC39 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C39.ttf"));
            StorageFile fileFontD39 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D39.ttf"));
            StorageFile fileFontE39 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E39.ttf"));
            StorageFile fileFontF39 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F39.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA39.Path, "C:\\Windows\\FONTS\\Font0A39.ttf");
                cs.NRSCopyFile(fileFontB39.Path, "C:\\Windows\\FONTS\\Font0B39.ttf");
                cs.NRSCopyFile(fileFontC39.Path, "C:\\Windows\\FONTS\\Font0C39.ttf");
                cs.NRSCopyFile(fileFontD39.Path, "C:\\Windows\\FONTS\\Font0D39.ttf");
                cs.NRSCopyFile(fileFontE39.Path, "C:\\Windows\\FONTS\\Font0E39.ttf");
                cs.NRSCopyFile(fileFontF39.Path, "C:\\Windows\\FONTS\\Font0F39.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A39.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0A39.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0A39.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0A39.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0A39.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A39.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "Qarmic Sans"));
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
        private async void BotaoFont40(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA40 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A40.ttf"));
            StorageFile fileFontB40 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B40.ttf"));
            StorageFile fileFontC40 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C40.ttf"));
            StorageFile fileFontD40 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D40.ttf"));
            StorageFile fileFontE40 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E40.ttf"));
            StorageFile fileFontF40 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F40.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA40.Path, "C:\\Windows\\FONTS\\Font0A40.ttf");
                cs.NRSCopyFile(fileFontB40.Path, "C:\\Windows\\FONTS\\Font0B40.ttf");
                cs.NRSCopyFile(fileFontC40.Path, "C:\\Windows\\FONTS\\Font0C40.ttf");
                cs.NRSCopyFile(fileFontD40.Path, "C:\\Windows\\FONTS\\Font0D40.ttf");
                cs.NRSCopyFile(fileFontE40.Path, "C:\\Windows\\FONTS\\Font0E40.ttf");
                cs.NRSCopyFile(fileFontF40.Path, "C:\\Windows\\FONTS\\Font0F40.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "Font0A40.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0A40.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Italic (TrueType)";
                value = "Font0A40.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0A40.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0A40.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0A40.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A40.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "Rio Segoe"));
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
        private async void BotaoFont41(object sender, RoutedEventArgs e)
        {
            StorageFile fileFontA41 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0A41.ttf"));
            StorageFile fileFontB41 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0B41.ttf"));
            StorageFile fileFontC41 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0C41.ttf"));
            StorageFile fileFontD41 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0D41.ttf"));
            StorageFile fileFontE41 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0E41.ttf"));
            StorageFile fileFontF41 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Letras/Font0F41.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data = loader.GetString("textData");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(fileFontA41.Path, "C:\\Windows\\FONTS\\Font0A41.ttf");
                cs.NRSCopyFile(fileFontB41.Path, "C:\\Windows\\FONTS\\Font0B41.ttf");
                cs.NRSCopyFile(fileFontC41.Path, "C:\\Windows\\FONTS\\Font0C41.ttf");
                cs.NRSCopyFile(fileFontD41.Path, "C:\\Windows\\FONTS\\Font0D41.ttf");
                cs.NRSCopyFile(fileFontE41.Path, "C:\\Windows\\FONTS\\Font0E41.ttf");
                cs.NRSCopyFile(fileFontF41.Path, "C:\\Windows\\FONTS\\Font0F41.ttf");
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Italic (TrueType)";
                value = "Font0A41.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Black (TrueType)";
                value = "Font0A41.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "Font0A41.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "Font0A41.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "Font0A41.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "Font0A41.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + "Rix Love Fool"));
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
     
        private async void BotaoRestaurar(object sender, RoutedEventArgs e)
        {
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var datas = loader.GetString("datas");
            var text = loader.GetString("textAcao");
            var font = loader.GetString("font");
            var restore = loader.GetString("restore");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                UInt32 REG_SZ = 1;
                UInt32 hKey = 1;
                String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                String key = "";
                String value = "";
                key = "Segoe UI (TrueType)";
                value = "segoeui.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Bold (TrueType)";
                value = "segoeuib.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Italic (TrueType)";
                value = "segoeuil.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Italic (TrueType)";
                value = "segoeuii.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Light (TrueType)";
                value = "segoeuil.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semibold (TrueType)";
                value = "seguisb.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                key = "Segoe UI Semilight (TrueType)";
                value = "segoeuisl.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(font + restore));
                    toastTextElements[1].AppendChild(toastXml.CreateTextNode(datas));
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
        private void Button_Holding(object sender, HoldingRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

    }
}