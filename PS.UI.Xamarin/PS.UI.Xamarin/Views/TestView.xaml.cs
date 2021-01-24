using PS.UI.Shared.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PS.UI.Xamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestView : ContentPage
    {
        public TestView()
        {
            InitializeComponent();
            BindingContext = IoC.Get<TestViewModel>();
        }
    }
}