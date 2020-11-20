using PS.UI.Xamarin.Services;
using PS.UI.Xamarin.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PS.UI.Xamarin
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
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
