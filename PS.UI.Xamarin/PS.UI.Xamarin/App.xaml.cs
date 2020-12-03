using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace PS.UI.Xamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Container.Create();
            MainPage = Container.ServiceProvider.GetRequiredService<AppShell>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}