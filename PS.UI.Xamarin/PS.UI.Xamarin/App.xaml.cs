using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PS.UI.Shared;
using PS.UI.Xamarin.Views;
using System;
using Xamarin.Forms;

namespace PS.UI.Xamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            MainPage = new AppShell();
        }

        public IConfiguration Configuration { get; private set; }

        public IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void ConfigureServices(IServiceCollection services)
        {
            DIConfigure.Shared(services);
            services.AddSingleton<AboutPage>();
        }
    }
}