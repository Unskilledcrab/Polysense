using PS.Shared.HttpClients;
using PS.Shared.Models;
using PS.Shared.Models.Abstractions;
using PS.UI.Shared.Commands.CategorizeScrapedDataCommands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PS.UI.Shared.ViewModels
{
    public class CategorizeScrapedDataViewModel : BaseViewModel
    {
        public readonly ScraperTextClient scraperClient;

        public CategorizeScrapedDataViewModel(ScraperTextClient scraperClient)
        {
            this.scraperClient = scraperClient;

            Categories.Add(new Category() { Id = 0, Label = "Opinion" });
            Categories.Add(new Category() { Id = 1, Label = "Fact" });
            Categories.Add(new Category() { Id = 2, Label = "Deflection" });
            Categories.Add(new Category() { Id = 3, Label = "Gibberish" });
        }

        public CategorizeScrapedDataViewModel This => this;
        public CategorizeScrapedText CategorizeScrapedText { get; private set; } = new CategorizeScrapedText();
        public GetNextScraperText GetNextScraperText { get; private set; } = new GetNextScraperText();
        public string MyTitle { get; set; } = "Title";
        public IEnumerable<ScraperText> Excerpts { get; set; }
        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();

        public ScraperText SelectedExcerpt { get; set; }
        public Category SelectedCategory { get; set; }

        public async override Task OnUpdate(CancellationToken token)
        {
            //not being used!!
            //Need to disable the button while this processes somehow
            if (Excerpts == null)
            {
                Excerpts = await scraperClient.GetScraperTexts(token);
                SelectedExcerpt = Excerpts.FirstOrDefault();
            }
            else
                SelectedExcerpt = Excerpts.Where(x => x.Id > SelectedExcerpt.Id).FirstOrDefault();

            if (SelectedExcerpt == null)
            {
                Excerpts = await scraperClient.GetScraperTexts(token);
                SelectedExcerpt = Excerpts.FirstOrDefault();
            }
        }

        public class Category : BaseEntity
        {
            public string Label { get; set; }
        }
    }
}