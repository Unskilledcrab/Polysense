using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PS.UI.WPF.Views;
using Syncfusion.SfSkinManager;
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
            SfSkinManager.ApplyStylesOnApplication = true;
            Container.Create();
            var mainWindow = Container.ServiceProvider.GetRequiredService<TestWindow>();
            mainWindow.Show();

            //var catWindow = Container.ServiceProvider.GetRequiredService<CategorizeScrapedDataWindow>();
            //catWindow.Show();
        }
    }
}