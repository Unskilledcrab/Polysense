using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PS.UI.WPF.Views;
using System.Windows;

namespace PS.UI.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            Container.Create();
            var mainWindow = Container.ServiceProvider.GetRequiredService<TestWindow>();
            mainWindow.Show();

            var catWindow = ServiceProvider.GetRequiredService<CategorizeScrapedDataWindow>();
            catWindow.Show();
        }
    }
}