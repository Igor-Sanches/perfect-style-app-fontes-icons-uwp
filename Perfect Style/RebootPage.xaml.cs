using System.Runtime.InteropServices;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Perfect_Style
{
    public sealed partial class RebootPage : Page
    {
            public RebootPage()
            {
                this.InitializeComponent();
            ndtklib.NRPC rpc = new ndtklib.NRPC();
            rpc.Initialize();
            rpc.SystemReboot();
        }
         }


}

