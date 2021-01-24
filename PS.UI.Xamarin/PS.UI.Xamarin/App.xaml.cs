using Xamarin.Forms;

namespace PS.UI.Xamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            IoC.Create();
            MainPage = IoC.Get<AppShell>();
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