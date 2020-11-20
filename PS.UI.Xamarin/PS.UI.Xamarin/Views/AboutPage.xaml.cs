using PS.UI.Shared.ViewModels;
using Xamarin.Forms;

namespace PS.UI.Xamarin.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage(TestViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}