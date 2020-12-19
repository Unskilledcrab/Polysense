using PS.Shared.HttpClients;
using PS.UI.WPF.Extensions;
using Syncfusion.UI.Xaml.Kanban;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace PS.UI.WPF.ViewModels
{
    public class TestViewModel : Shared.ViewModels.TestViewModel
    {
        private bool isInitalized = false;

        public TestViewModel(ScraperTextClient scraperClient, TextCategoryClient categoryClient) : base(scraperClient, categoryClient)
        {
            Title = "Test Scraper Categorizer";
        }

        public KanbanColumnsCollection ScraperColumns { get; set; } = new KanbanColumnsCollection();

        public ObservableCollection<KanbanModel> ScraperTasks { get; set; } = new ObservableCollection<KanbanModel>();

        public async override Task OnUpdate(CancellationToken token)
        {
            await base.OnUpdate(token);
            Initalize();
            ScraperTasks.Clear();
            foreach (var item in ScrapedTexts)
                ScraperTasks.Add(item.ToKanbanCard());
        }

        private void Initalize()
        {
            if (isInitalized) return;
            foreach (var category in Categories)
                ScraperColumns.Add(category.ToKanbanColumn());
        }
    }
}