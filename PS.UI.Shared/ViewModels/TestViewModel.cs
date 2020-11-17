using Microsoft.Toolkit.Mvvm.Input;
using PS.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PS.UI.Shared.ViewModels
{
    public class TestViewModel : BaseViewModel
    {
        private readonly PolysenseClient httpClient;

        public IList<Politician> Politicians { get; set; }
        public ICommand UpdateCommand { get; set; }

        public TestViewModel(PolysenseClient httpClient)
        {
            UpdateCommand = new AsyncRelayCommand(Update);
            this.httpClient = httpClient;
        }

        private async Task GetPoliticians()
        {
            Politicians = await httpClient.GetAsync<IList<Politician>>("politicians");
        }

        public override async Task OnUpdate()
        {
            await GetPoliticians();
        }
    }
}
