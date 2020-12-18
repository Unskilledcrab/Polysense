using PS.Shared.HttpClients;
using PS.Shared.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace PS.UI.Shared.ViewModels
{
    public class TestViewModel : BaseViewModel
    {
        private readonly ScraperTextClient scraperClient;

        public TestViewModel(ScraperTextClient scraperClient)
        {
            this.scraperClient = scraperClient;
        }

        public IEnumerable<ScraperText> ScrapedText { get; set; }
        public ObservableCollection<ScraperText> s { get; set; }

        public override async Task OnUpdate(CancellationToken token)
        {
            await GetScrapedText(token);
        }

        private async Task GetScrapedText(CancellationToken token)
        {
            var response = await scraperClient.GetScraperTexts(token: token);
            ScrapedText = response;
        }
    }
}