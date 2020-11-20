using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PS.UI.Xamarin.ViewModels
{
    public class AboutViewModel : PS.UI.Shared.ViewModels.BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

        public ICommand OpenWebCommand { get; }

        public override Task OnUpdate(CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}