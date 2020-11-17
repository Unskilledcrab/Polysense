using PS.UI.Shared.ViewModels;
using System.Windows;

namespace PS.UI.WPF.Views
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public TestWindow(TestViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}