using PS.Shared.HttpClients;
using PS.Shared.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PS.UI.Shared.ViewModels
{
    public class TestViewModel : BaseUpdateViewModel
    {
        protected readonly ScraperTextClient scraperClient;
        protected readonly TextCategoryClient categoryClient;

        public TestViewModel(ScraperTextClient scraperClient, TextCategoryClient categoryClient)
        {
            this.scraperClient = scraperClient;
            this.categoryClient = categoryClient;
        }

        public IEnumerable<ScraperText> ScrapedTexts { get; set; } = new List<ScraperText>();
        public IEnumerable<TextCategory> Categories { get; set; } = new List<TextCategory>();

        public override async Task OnUpdate(CancellationToken token)
        {
            await GetScrapedText(token);
            await GetCategories(token);
        }

        protected async Task GetCategories(CancellationToken token)
        {
            var response = await categoryClient.GetEntities(token: token);
            Categories = response.Data;
        }

        protected async Task GetScrapedText(CancellationToken token)
        {
            var response = await scraperClient.GetEntities(token: token);
            ScrapedTexts = response.Data;
        }
    }
}