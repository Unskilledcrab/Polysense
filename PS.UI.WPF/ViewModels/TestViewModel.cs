using PS.Shared.HttpClients;
using PS.UI.WPF.Models;
using Syncfusion.UI.Xaml.Kanban;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace PS.UI.WPF.ViewModels
{
    public class TestViewModel : Shared.ViewModels.TestViewModel
    {
        public TestViewModel(ScraperTextClient scraperClient) : base(scraperClient)
        {
            Title = "Test Scraper Categorizer";
            ScraperColumns.Add(new KanbanColumn
            {
                Title = "Uncategorized",
                Categories = "Uncategorized"
            });
        }

        public KanbanColumnsCollection ScraperColumns { get; set; } = new KanbanColumnsCollection();
        public ObservableCollection<KanbanWrapper> ScraperTasks { get; set; } = new ObservableCollection<KanbanWrapper>();

        public async override Task OnUpdate(CancellationToken token)
        {
            await base.OnUpdate(token);
            foreach (var item in ScrapedTexts)
                ScraperTasks.Add(new KanbanWrapper(item));
        }
    }
}