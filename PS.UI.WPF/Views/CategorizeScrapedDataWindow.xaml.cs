using PS.UI.Shared.ViewModels;
using System.Diagnostics;
using System.Windows;

namespace PS.UI.WPF.Views
{
    /// <summary>
    /// Interaction logic for CategorizeScrapedData.xaml
    /// </summary>
    public partial class CategorizeScrapedDataWindow : Window
    {
        public CategorizeScrapedDataWindow(CategorizeScrapedDataViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        public void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            //temp for testing
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }
    }
}