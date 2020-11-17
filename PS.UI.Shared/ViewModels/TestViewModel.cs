using PS.Shared.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PS.UI.Shared.ViewModels
{
    public class TestViewModel : BaseViewModel
    {
        private readonly PolysenseClient httpClient;

        public TestViewModel(PolysenseClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public IList<Politician> Politicians { get; set; }

        public override async Task OnUpdate(CancellationToken token)
        {
            await GetPoliticians(token);
        }

        private async Task GetPoliticians(CancellationToken token)
        {
            Politicians = await httpClient.GetAsync<IList<Politician>>("politicians", token);
        }
    }
}