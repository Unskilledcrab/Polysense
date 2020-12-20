using Microsoft.Toolkit.Mvvm.Input;
using PS.Shared.HttpClients;
using PS.UI.WPF.Extensions;
using Syncfusion.UI.Xaml.Kanban;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PS.UI.WPF.ViewModels
{
    public class TestViewModel : Shared.ViewModels.TestViewModel
    {
        private bool isInitalized = false;

        public TestViewModel(ScraperTextClient scraperClient, TextCategoryClient categoryClient) : base(scraperClient, categoryClient)
        {
            Title = "Test Scraper Categorizer";
            CardDragEndCommand = new AsyncRelayCommand<KanbanDragEndEventArgs>(OnCardDragEnd);
        }

        public ICommand CardDragEndCommand { get; set; }

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

        internal async Task OnCardDragEnd(KanbanDragEndEventArgs e)
        {
            if (e.IsCancel || e.SelectedColumnIndex == e.TargetColumnIndex) return;

            var card = e.SelectedCard.Content as KanbanModel;
            var scraperText = await scraperClient.GetScraperText(Convert.ToInt32(card.ID));
            scraperText.Category.Name = card.Category.ToString();
            await scraperClient.UpdateScraperText(scraperText);
        }

        private void Initalize()
        {
            if (isInitalized) return;
            foreach (var category in Categories)
                ScraperColumns.Add(category.ToKanbanColumn());
        }
    }
}