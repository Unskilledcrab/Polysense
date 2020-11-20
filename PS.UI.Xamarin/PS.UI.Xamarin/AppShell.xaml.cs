using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PS.UI.Xamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }
    }
}