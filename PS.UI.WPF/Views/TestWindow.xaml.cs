using PS.UI.WPF.ViewModels;
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