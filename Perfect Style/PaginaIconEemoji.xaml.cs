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

// Projeto Perfect Style Igor Sanches Anjatuba, MA, Brasil, Copyright © 2017
// Início do Projeto em 8 de outubro de 2017

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
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += PaginaIconEemoji_BackPressed; //Botão voltar
        }
        //Botão voltar
        void PaginaIconEemoji_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
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
            if (MySplitView.IsPaneOpen == false)
            {

            }

            else if (MySplitView.IsPaneOpen == true)
            {
                MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
            }
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

        //Area 
        private async void AddFont2(object sender, RoutedEventArgs e)
        {

            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = PickerViewMode.List;
            picker.SuggestedStartLocation = PickerLocationId.ComputerFolder;
            picker.FileTypeFilter.Add(".ttf");
            Windows.Storage.StorageFile File2 = await picker.PickSingleFileAsync();
            if (File2 != null)
            {
                var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
                var content = loader.GetString("textContent");
                var label0 = loader.GetString("textLabel0");
                var label1 = loader.GetString("textLabel1");
                var data = loader.GetString("textData");
                var text = loader.GetString("textAcao");
                var icon2 = loader.GetString("icon2");
                var add = loader.GetString("addfont");
                var messageDialog = new MessageDialog(content, text);
                messageDialog.Commands.Add(new UICommand(label0, (command) =>
                {
                    cs.NRSCopyFile(File2.Path, "C:\\Windows\\FONTS\\AddIcon.ttf");
                    UInt32 REG_SZ = 1;
                    UInt32 hKey = 1;
                    String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                    String key = "";
                    String value = "";
                    key = "Segoe MDL2 Assets (TrueType)";
                    value = "AddIcon.ttf";
                    rpc.rset(hKey, path, key, REG_SZ, value, 0);
                    {
                        ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                        XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                        XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                        toastTextElements[0].AppendChild(toastXml.CreateTextNode(icon2 + add));
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
        private async void AddFont3(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = PickerViewMode.List;
            picker.SuggestedStartLocation = PickerLocationId.ComputerFolder;
            picker.FileTypeFilter.Add(".ttf");
            Windows.Storage.StorageFile File3 = await picker.PickSingleFileAsync();
            if (File3 != null)
            {
                var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
                var content = loader.GetString("textContent");
                var label0 = loader.GetString("textLabel0");
                var label1 = loader.GetString("textLabel1");
                var data = loader.GetString("textData");
                var text = loader.GetString("textAcao");
                var emojis = loader.GetString("emojis");
                var add = loader.GetString("addfont");
                var messageDialog = new MessageDialog(content, text);
                messageDialog.Commands.Add(new UICommand(label0, (command) =>
                {
                    cs.NRSCopyFile(File3.Path, "C:\\Windows\\FONTS\\AddFont.ttf");
                    UInt32 REG_SZ = 1;
                    UInt32 hKey = 1;
                    String path = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
                    String key = "";
                    String value = "";
                    key = "Segoe UI Emoji (TrueType)";
                    value = "AddEmoji.ttf";
                    rpc.rset(hKey, path, key, REG_SZ, value, 0);
                    {
                        ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                        XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                        XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                        toastTextElements[0].AppendChild(toastXml.CreateTextNode(emojis + add));
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

        private async void BotaoEmoji1(object sender, RoutedEventArgs e)
        {
            StorageFile FontEmoji0A1 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Fontes/Emoji/FontEmoji0A1.ttf"));

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var content = loader.GetString("textContent");
            var label0 = loader.GetString("textLabel0");
            var label1 = loader.GetString("textLabel1");
            var data3 = loader.GetString("Data3");
            var emoji = loader.GetString("emoj");
            var text = loader.GetString("textAcao");
            var messageDialog = new MessageDialog(content, text);
            messageDialog.Commands.Add(new UICommand(label0, (command) =>
            {
                cs.NRSCopyFile(FontEmoji0A1.Path, "C:\\Windows\\FONTS\\FontEmoji0A1.ttf");
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
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode("Emoji" + emoji));
                    toastTextElements[1].AppendChild(toastXml.CreateTextNode(data3));
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
            var datas2 = loader.GetString("datas2");
            var text = loader.GetString("textAcao");
            var restore2 = loader.GetString("restore2");
            var emojis = loader.GetString("emojis");
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
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(emojis + restore2));
                    toastTextElements[1].AppendChild(toastXml.CreateTextNode(datas2));
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
            var data3 = loader.GetString("Data3");
            var text = loader.GetString("textAcao");
            var icon = loader.GetString("icon");
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
                key = "Segoe MDL2 Assets (TrueType)";
                value = "FontIcon0A1.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(icon + "Android LolliPop"));
                    toastTextElements[1].AppendChild(toastXml.CreateTextNode(data3));
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
            var data3 = loader.GetString("Data3");
            var text = loader.GetString("textAcao");
            var icon = loader.GetString("icon");
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
                key = "Segoe MDL2 Assets (TrueType)";
                value = "FontIcon0A2.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(icon + "IOS"));
                    toastTextElements[1].AppendChild(toastXml.CreateTextNode(data3));
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
            var data3 = loader.GetString("Data3");
            var text = loader.GetString("textAcao");
            var icon = loader.GetString("icon");
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
                key = "Segoe MDL2 Assets (TrueType)";
                value = "FontIcon0A4.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(icon + "Color"));
                    toastTextElements[1].AppendChild(toastXml.CreateTextNode(data3));
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
            var data3 = loader.GetString("Data3");
            var text = loader.GetString("textAcao");
            var icon = loader.GetString("icon");
            var batere = loader.GetString("batere");
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
                key = "Segoe MDL2 Assets (TrueType)";
                value = "FontIcon0A3.ttf";
                rpc.rset(hKey, path, key, REG_SZ, value, 0);
                {
                    ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
                    XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(icon + batere));
                    toastTextElements[1].AppendChild(toastXml.CreateTextNode(data3));
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
            var datas2 = loader.GetString("datas2");
            var text = loader.GetString("textAcao");
            var icon = loader.GetString("icon");
            var restore2 = loader.GetString("restore2");
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
                    toastTextElements[0].AppendChild(toastXml.CreateTextNode(icon + restore2));
                    toastTextElements[1].AppendChild(toastXml.CreateTextNode(datas2));
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