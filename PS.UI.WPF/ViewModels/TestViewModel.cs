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
            // If the drag was cancelled or dragged to the same column
            if (e.IsCancel || e.SelectedColumnIndex == e.TargetColumnIndex) return;

            var card = e.SelectedCard.Content as KanbanModel;

            // Get the scraperText model from the db for this card
            var scraperText = await scraperClient.GetEntity(Convert.ToInt32(card.ID));

            // Update the cards category to the dropped category
            scraperText.Category = new PS.Shared.Models.TextCategory
            {
                Name = card.Category.ToString()
            };

            // Update the database
            await scraperClient.UpdateEntity(scraperText);
        }

        private void Initalize()
        {
            if (isInitalized) return;
            foreach (var category in Categories)
                ScraperColumns.Add(category.ToKanbanColumn());
            isInitalized = true;
        }
    }
}