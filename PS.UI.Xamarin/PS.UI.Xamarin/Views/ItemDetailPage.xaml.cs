using PS.UI.Xamarin.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace PS.UI.Xamarin.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}