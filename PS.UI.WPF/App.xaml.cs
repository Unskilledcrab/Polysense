using Microsoft.Extensions.Configuration;
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
            IoC.Create();
            var mainWindow = IoC.Get<TestWindow>();
            mainWindow.Show();

            //var catWindow = Container.ServiceProvider.GetRequiredService<CategorizeScrapedDataWindow>();
            //catWindow.Show();
        }
    }
}